import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  HttpCode,
} from '@nestjs/common';
import { ReservationsService } from './reservations.service';
import { CreateReservationsDto } from './dto/create-reservations.dto';

@Controller('reservations')
export class ReservationsController {
  constructor(private readonly reservationsService: ReservationsService) {}

  @Post()
  @HttpCode(201)
  async create(@Body() createReservationDto: CreateReservationsDto) {
    console.log('Reservation: Create');
    console.log(createReservationDto);
    return await this.reservationsService.create(createReservationDto);
  }

  @Get(':id')
  async findOne(@Param('id') id: string) {
    console.log('Reservation: Find');
    console.log(id);
    return await this.reservationsService.findOne(id);
  }

  @Delete(':id')
  @HttpCode(204)
  async remove(@Param('id') id: string) {
    console.log('Reservation: Remove');
    console.log(id);
    return await this.reservationsService.remove(id);
  }
}
