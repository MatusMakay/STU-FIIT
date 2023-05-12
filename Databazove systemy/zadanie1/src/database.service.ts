import { Injectable, Inject } from '@nestjs/common';
import { Pool } from 'pg';
import { queryGetTravelCompanion, queryGetDelayedFlights, queryMaxPassengers, queryPlannedLinks, queryDestinationFromAirport, queryLoadForLink, queryGetDetailFlight, queryStatisticLoadForWeek, queryAirTime, queryProfitDays, queryStatisticForSeats } from './queries/query';
import { AirTime } from './interface/interface.dto';



@Injectable()
export class DatabaseService {

  constructor(@Inject('POSTGRES_DBS') private readonly pool: Pool) {
    this.pool.connect()
  }

  public async getVersion(): Promise<any> {

    return this.pool.query('SELECT version()')
  }

  public async getDelayedFlights(minMinutes: String) {

    const result = await this.pool.query(queryGetDelayedFlights, [minMinutes]);

    return {
      results: result.rows
    }
  }

  public async detailedFlightInfo(bookId: string) {
    const result = await this.pool.query(queryGetDetailFlight, [bookId]);

    return {
      "result": result.rows[0]
    };
  }

  public async getTravelCompanions(passengerID: string): Promise<any> {

    const result = await this.pool.query(queryGetTravelCompanion, [passengerID]);

    return {
      "results": result.rows
    };
  }

  public async maxLinkPassanger(limit: String): Promise<any> {

    const result = await this.pool.query(queryMaxPassengers, [limit]);

    return {
      "results": result.rows
    };

  }

  public async plannedLink(day: String, aircraftCode: string): Promise<any> {

    const result = await this.pool.query(queryPlannedLinks, [day, aircraftCode]);

    return {
      "results": result.rows
    };
  }

  public async destinationAirport(departureAirport: String): Promise<any> {

    let result;
    result = await this.pool.query(queryDestinationFromAirport, [departureAirport]);

    return result.rows[0]
  }

  public async loadSpecificLink(flightNumber: String): Promise<any> {

    const result = await this.pool.query(queryLoadForLink, [flightNumber]);

    return {
      "results": result.rows
    };
  }

  public async statisticForWeek(flightNo: string) {

    const result = await this.pool.query(queryStatisticLoadForWeek, [flightNo]);

    return result.rows[0]


  }

  public async orderStatisticForSeat(aircraftCode: string, seatChoice: string) {

    const result = await this.pool.query(queryStatisticForSeats, [aircraftCode, seatChoice]);

    return {
      result: result.rows[0]
    }
  }

  public async timeInAir(bookRef: string) {

    let tmpArr = await this.pool.query(queryAirTime, [bookRef]);
    let result = []

    let move = 0

    tmpArr.rows.forEach((row: AirTime, idx) => {

      if (idx == 0) {
        let tmp: AirTime = {
          passenger_name: null,
          ticket_no: null,
          flights: []
        }
        tmp.passenger_name = row.passenger_name
        tmp.ticket_no = row.ticket_no
        tmp.flights.push(row.flights)
        result.push(tmp)
      }
      else {
        if (row.passenger_name == result[move].passenger_name) {
          result[move].flights.push(row.flights)
        }
        else {
          let tmp: AirTime = {
            passenger_name: null,
            ticket_no: null,
            flights: []
          }
          tmp.passenger_name = row.passenger_name
          tmp.ticket_no = row.ticket_no
          tmp.flights.push(row.flights)
          result.push(tmp)
          move += 1
        }
      }


    });



    return {
      results: result
    }
  }

  public async profitDays(airportCode: string) {

    const result = await this.pool.query(queryProfitDays, [airportCode]);
    return {
      results: result.rows
    }
  }
}
