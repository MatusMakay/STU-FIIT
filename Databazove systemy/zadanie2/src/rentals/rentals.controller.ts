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
import { RentalsService } from './rentals.service';
import { CreateRentalsDto } from './dto/create-rentals.dto';
import { UpdateRentalsDto } from './dto/update-rentals.dto';

@Controller('rentals')
export class RentalsController {
  constructor(private readonly rentalsService: RentalsService) {}

  @Post()
  @HttpCode(201)
  async create(@Body() createRentalDto: CreateRentalsDto) {
    console.log('Rentals: Post');
    console.log(createRentalDto);
    return await this.rentalsService.create(createRentalDto);
  }

  @Get(':id')
  async findOne(@Param('id') id: string) {
    console.log('Rentals: Find');
    console.log(id);
    return await this.rentalsService.findOne(id);
  }

  @Patch(':id')
  async update(
    @Param('id') id: string,
    @Body() updateRentalDto: UpdateRentalsDto,
  ) {
    console.log('Rentals: Update');
    console.log(updateRentalDto);
    return await this.rentalsService.update(id, updateRentalDto);
  }
}
