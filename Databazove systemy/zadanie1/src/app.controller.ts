import { Controller, Get, Param, Query } from '@nestjs/common';
import { DatabaseService } from './database.service';

@Controller()
export class AppController {

  constructor(private readonly databaseService: DatabaseService) { }


  @Get()
  getHome() {
    return '<h1> Dbs Zadanie c.1 </h1><p>Made by love</p><p>Matus Makay</p>'
  }

  @Get('v1/status')
  async getVersion(): Promise<any> {

    let version: any

    version = await this.databaseService.getVersion()

    version = version.rows[0].version

    return {
      version: version
    }

  }

  @Get('v1/passengers/:passengerID/companions')
  async getFlightCompanion(@Param() params: object): Promise<any> {

    let companions: any

    companions = await this.databaseService.getTravelCompanions(params["passengerID"]);

    return companions;

  }

  @Get('/v1/bookings/:bookingId')
  async getDetailFlight(@Param() params: object): Promise<any> {

    let detail: any
    //coment len aby bolodobre

    detail = await this.databaseService.detailedFlightInfo(params["bookingId"])

    return detail;

  }

  @Get('v1/flights/late-departure/:delay')
  async getDelayedFlights(@Param() params: object) {

    let delayedFlights: any

    delayedFlights = await this.databaseService.getDelayedFlights(params["delay"]);

    return delayedFlights;
  }

  @Get('v1/top-airlines')
  async getMaxLinkPassenger(@Query('limit') limit: string) {

    let maxPassangerLink: any

    maxPassangerLink = await this.databaseService.maxLinkPassanger(limit);

    return maxPassangerLink;
  }

  @Get('v1/departures')
  async getPlannedLink(@Query('airport') airport: string, @Query('day') day: string) {

    let plannedLinks: any

    plannedLinks = await this.databaseService.plannedLink(day, airport);

    return plannedLinks;
  }

  @Get('v1/airports/:airport/destinations')
  async getDestinationFromAirports(@Param() params: object) {

    let destinations: any

    destinations = await this.databaseService.destinationAirport(params["airport"]);

    return destinations;
  }

  @Get('v1/airlines/:flight_no/load')
  async getLoadForSpecLink(@Param() params: object) {

    let load: any

    load = await this.databaseService.loadSpecificLink(params["flight_no"]);

    return load;
  }

  @Get('/v1/airlines/:flightNo/load-week')
  async getStatisticOfWeek(@Param() params: object) {

    let plannedLinks: any

    plannedLinks = await this.databaseService.statisticForWeek(params["flightNo"])

    return plannedLinks;
  }

  @Get('/v3/aircrafts/:aircraftCode/seats/:seatChoice')
  async getKorderForSeat(@Param() params: object) {

    let orderedSeats: any

    orderedSeats = await this.databaseService.orderStatisticForSeat(params["aircraftCode"], params["seatChoice"])

    return orderedSeats;

  }
  @Get('/v3/air-time/:bookRef')
  async getAirTime(@Param() params: object) {

    let timeInAir: any

    timeInAir = await this.databaseService.timeInAir(params["bookRef"])

    return timeInAir;
  }

  @Get('/v3/aircrafts/:aircraftCode/top-incomes')
  async getMaxProfitDay(@Param() params: object) {

    let maxProfitDays: any

    maxProfitDays = await this.databaseService.profitDays(params["aircraftCode"])

    return maxProfitDays
  }
}
