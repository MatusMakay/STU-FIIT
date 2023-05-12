const queryGetTravelCompanion = `
SELECT  bookings.tickets.passenger_id AS id,  bookings.tickets.passenger_name AS name, count(bookings.tickets.passenger_name)::int AS flights_count, ARRAY_AGG(bookings.ticket_flights.flight_id ORDER BY bookings.ticket_flights.flight_id ASC) as flights  FROM bookings.tickets 
FULL JOIN bookings.ticket_flights ON bookings.tickets.ticket_no = bookings.ticket_flights.ticket_no WHERE bookings.ticket_flights.flight_id = ANY
(
        SELECT flight_id FROM bookings.tickets
        FULL JOIN bookings.ticket_flights ON bookings.tickets.ticket_no = bookings.ticket_flights.ticket_no
        WHERE passenger_id=$1::text ORDER BY passenger_id ASC

) AND  bookings.tickets.passenger_id <> $1
GROUP BY passenger_id, passenger_name ORDER BY flights_count DESC, bookings.tickets.passenger_id ASC;
`;

const queryGetDetailFlight = `
SELECT book_info.book_ref as id, to_char(book_info.book_date AT TIME ZONE 'UTC', 'YYYY-MM-DD"T"HH24:MI:SS+00:00') AS book_date, 

   json_agg(json_build_object(
      'id', tick_flights.ticket_no,
      'passenger_id', passanger_info.passenger_id,
      'passenger_name', passanger_info.passenger_name,
      'boarding_no', passes_info.boarding_no,
      'flight_no', flights_info.flight_no,
      'seat', passes_info.seat_no,
      'aircraft_code', flights_info.aircraft_code,
      'arrival_airport', flights_info.arrival_airport,
      'departure_airport', flights_info.departure_airport,
      'scheduled_arrival', flights_info.scheduled_arrival,
      'scheduled_departure', flights_info.scheduled_departure
  ) ORDER BY tick_flights.ticket_no ASC, passes_info.boarding_no ASC) AS boarding_passes FROM bookings.boarding_passes as passes_info
    JOIN (SELECT ticket_no, flight_id FROM bookings.ticket_flights) as tick_flights ON tick_flights.ticket_no = passes_info.ticket_no AND tick_flights.flight_id = passes_info.flight_id
    JOIN (SELECT flight_id, flight_no, aircraft_code, arrival_airport, departure_airport, scheduled_arrival, scheduled_departure FROM bookings.flights) AS flights_info ON flights_info.flight_id = tick_flights.flight_id
    JOIN (SELECT book_ref, ticket_no, passenger_id, passenger_name FROM bookings.tickets) AS passanger_info ON passanger_info.ticket_no = tick_flights.ticket_no
    JOIN (SELECT book_ref, book_date FROM bookings.bookings) AS book_info ON book_info.book_ref = passanger_info.book_ref
    WHERE book_info.book_ref = $1
    GROUP BY book_info.book_ref, book_info.book_date;
`
const queryGetDelayedFlights = `
SELECT
    flight_id,
    flight_no,
    (EXTRACT (EPOCH FROM (actual_departure - scheduled_departure)) / 60 )::integer AS delay
    FROM bookings.flights WHERE (EXTRACT (EPOCH FROM (actual_departure - scheduled_departure)) / 60 ) >= $1 ORDER BY delay DESC;
`
const queryMaxPassengers = `
SELECT flight_no, count(bookings.ticket_flights.flight_id)::int  AS count FROM bookings.ticket_flights JOIN (SELECT flight_no, flight_id, actual_departure, status FROM bookings.flights WHERE status = 'Arrived') as number_records ON number_records.flight_id = bookings.ticket_flights.flight_id
WHERE actual_departure IS NOT NULL  GROUP BY flight_no ORDER BY count DESC LIMIT $1;
`

const queryPlannedLinks = `
SELECT flight_id, flight_no,  to_char(scheduled_departure AT TIME ZONE 'UTC', 'YYYY-MM-DD"T"HH24:MI:SS+00:00') AS scheduled_departure
FROM bookings.flights
WHERE  actual_departure is NULL AND (EXTRACT('ISODOW' FROM scheduled_departure)) = $1 AND flights.departure_airport = $2
ORDER BY scheduled_departure ASC , flight_id;
`

const queryDestinationFromAirport = `
SELECT json_agg(result.arrival_airport) as results  FROM (SELECT DISTINCT arrival_airport FROM bookings.flights WHERE departure_airport = $1 GROUP BY arrival_airport ORDER BY  arrival_airport ASC) AS result;
`

const queryLoadForLink = `
SELECT bookings.ticket_flights.flight_id AS id, seats_capacity.aircraft_capacity,  COUNT(ticket_flights.flight_id)::int as load, ROUND((COUNT(ticket_flights.flight_id)::float/seats_capacity.aircraft_capacity::float *100)::numeric, 2)::float AS percentage_load FROM
bookings.ticket_flights
            JOIN (SELECT flight_id, aircraft_code, flight_no FROM bookings.flights WHERE flight_no = $1) AS flightsCode ON ticket_flights.flight_id = flightsCode.flight_id
            JOIN (SELECT aircrafts_data.aircraft_code, COUNT(seats.aircraft_code)::int AS aircraft_capacity FROM bookings.seats
            JOIN bookings.aircrafts_data ON seats.aircraft_code = aircrafts_data.aircraft_code GROUP BY seats.aircraft_code, aircrafts_data.aircraft_code) AS seats_capacity ON flightsCode.aircraft_code = seats_capacity.aircraft_code
            GROUP BY ticket_flights.flight_id, seats_capacity.aircraft_capacity ORDER BY ticket_flights.flight_id ASC;
`

const queryStatisticLoadForWeek = `
SELECT
    json_build_object(
        'flight_no',  flights.flight_no,
        'monday', round(avg(case when extract(isodow from flights.scheduled_departure) = 1 then percentage_load end), 2),
        'tuesday', round(avg(case when extract(isodow from flights.scheduled_departure) = 2 then percentage_load+0.01 end), 2),
        'wednesday', round(avg(case when extract(isodow from flights.scheduled_departure) = 3 then percentage_load end), 2),
        'thursday', round(avg(case when extract(isodow from flights.scheduled_departure) = 4 then percentage_load end), 2),
        'friday', round(avg(case when extract(isodow from flights.scheduled_departure) = 5 then percentage_load end), 2),
        'saturday', round(avg(case when extract(isodow from flights.scheduled_departure) = 6 then percentage_load end), 2),
        'sunday', round(avg(case when extract(isodow from flights.scheduled_departure) = 7 then percentage_load end), 2)
    ) as result
FROM (
    SELECT
        flights.flight_no,
        flights.scheduled_departure,
        round(((count(ticket_flights.flight_id)::float / count_seats.aircraft_capacity::float)::float * 100)::numeric, 2) as percentage_load
    FROM bookings.flights
    JOIN (
        SELECT
            air_data.aircraft_code,
            COUNT(seats.aircraft_code) AS aircraft_capacity
        FROM bookings.seats AS seats
        JOIN bookings.aircrafts_data AS air_data ON seats.aircraft_code = air_data.aircraft_code
        GROUP BY seats.aircraft_code, air_data.aircraft_code
    ) AS count_seats ON bookings.flights.aircraft_code = count_seats.aircraft_code
    JOIN bookings.ticket_flights ON flights.flight_id = ticket_flights.flight_id
    WHERE flights.flight_no = $1
    GROUP BY flights.flight_id, flights.flight_no, flights.scheduled_departure, count_seats.aircraft_capacity
) as flights
GROUP BY flights.flight_no;
`

const queryStatisticForSeats = `
SELECT
    rankSeatsSelect.seat_no as seat,
    count(rankSeatsSelect.ran)::int as count
FROM
    (
        SELECT
            dense_rank() OVER(partition by filter_flights.flight_id ORDER BY book_date) as ran,
            seat_no
        FROM
            (
                SELECT
                    seat_no,
                    tf.ticket_no,
                    fl.flight_id
                FROM bookings.boarding_passes as bp
                    JOIN (SELECT ticket_no, flight_id FROM bookings.ticket_flights) AS tf ON tf.flight_id = bp.flight_id and tf.ticket_no = bp.ticket_no
                    JOIN (SELECT aircraft_code, flight_id FROM bookings.flights) as fl ON fl.flight_id = tf.flight_id
                WHERE fl.aircraft_code = $1
            ) as filter_flights
        JOIN (SELECT book_ref, ticket_no FROM bookings.tickets) as tc on tc.ticket_no = filter_flights.ticket_no
        JOIN (SELECT book_date, book_ref FROM bookings.bookings) as bk ON bk.book_ref = tc.book_ref
    ) as rankSeatsSelect
WHERE rankSeatsSelect.ran = $2
GROUP BY rankSeatsSelect.seat_no
order by count DESC
LIMIT 1;
`

const queryAirTime = `
SELECT
    innerSelect.ticket_no,
    innerSelect.passenger_name,
    json_build_object(
    'departure_airport',innerSelect.departure_airport,
    'arrival_airport', innerSelect.arrival_airport,
    'flight_time', regexp_replace(innerSelect.flight_time, '^0', ''),
    'total_time', regexp_replace(to_char(innerSelect.total_time_wrong_format, 'HH24:MI:SS'), '^0', '')
    ) as flights
FROM
    (
        SELECT
            bp.ticket_no,
            tc.passenger_name,
            departure_airport,
            arrival_airport,
            flight_time,
            SUM(substraction) OVER (PARTITION BY passenger_name ORDER BY actual_departure ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) as total_time_wrong_format
        FROM bookings.boarding_passes as bp
            JOIN (SELECT ticket_no, flight_id FROM bookings.ticket_flights) AS tf ON tf.flight_id = bp.flight_id and tf.ticket_no = bp.ticket_no
            JOIN (SELECT aircraft_code, flight_id, actual_departure, actual_arrival, to_char((actual_arrival - actual_departure), 'HH24:MI:SS') as flight_time,(actual_arrival - actual_departure) as substraction,  departure_airport, arrival_airport FROM bookings.flights) as fl ON fl.flight_id = tf.flight_id
            JOIN (SELECT book_ref, ticket_no, passenger_name FROM bookings.tickets) as tc on tc.ticket_no = tf.ticket_no
            JOIN (SELECT book_date, book_ref FROM bookings.bookings) as bk ON bk.book_ref = tc.book_ref
        WHERE bk.book_ref = $1 and fl.actual_arrival IS NOT NULL and fl.actual_departure IS NOT NULL
    ) as innerSelect
ORDER BY innerSelect.ticket_no ASC
`
const queryProfitDays = `
SELECT
    lastSelect.day,
    lastSelect.month,
    lastSelect.total_amount::int
FROM
    (
        SELECT
            outerSelect.month,
            outerSelect.total_amount,
            outerSelect.day,
            MAX(total_amount) OVER (PARTITION BY outerSelect.month ROWS BETWEEN UNBOUNDED PRECEDING AND UNBOUNDED FOLLOWING) AS max
        FROM
            (
                SELECT
                    innerSelect.month,
                    innerSelect.day,
                    total_amount
                FROM
                    (
                        SELECT
                          fl.month,
                          fl.day,
                          SUM(amount) OVER (PARTITION BY fl.month, fl.day) AS total_amount
                        FROM bookings.ticket_flights as tf
                          JOIN (SELECT aircraft_code, flight_id, actual_departure, EXTRACT(DAY FROM actual_departure) AS day, to_char(actual_departure, 'YYYY-FMMM') as month  FROM bookings.flights) AS fl ON fl.flight_id = tf.flight_id
                        WHERE aircraft_code = $1 AND actual_departure IS NOT NULL
                    ) AS innerSelect
                GROUP BY innerSelect.month, innerSelect.day, total_amount
            ) AS outerSelect
    ) AS lastSelect
WHERE lastSelect.total_amount = lastSelect.max
ORDER BY lastSelect.total_amount DESC
`

export {
    queryGetTravelCompanion,
    queryGetDelayedFlights,
    queryMaxPassengers,
    queryPlannedLinks,
    queryDestinationFromAirport,
    queryLoadForLink,
    queryGetDetailFlight,
    queryStatisticLoadForWeek,
    queryStatisticForSeats,
    queryAirTime,
    queryProfitDays,
}