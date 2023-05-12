import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  HttpCode,
  HttpException,
} from '@nestjs/common';
import { CustomersService } from './customers.service';
import { CreateCustomerDto } from './dto/create-customer.dto';
import { UpdateCustomerDto } from './dto/update-customer.dto';
import { ValidateService } from './validation.service';
import { validate } from 'class-validator';

@Controller('users')
export class CustomersController {
  constructor(
    private readonly customersService: CustomersService,
    private readonly validationService: ValidateService,
  ) {}

  @Post()
  @HttpCode(201)
  async create(@Body() createCustomerDto: CreateCustomerDto) {
    console.log('Create: Customers');
    console.log(createCustomerDto);

    if (
      createCustomerDto.email &&
      !this.validationService.emailValidation(createCustomerDto.email)
    ) {
      throw new HttpException('Email is not valid', 400);
    }

    if (createCustomerDto.email == undefined) {
      throw new HttpException('Email is not valid', 400);
    }

    return await this.customersService.create(createCustomerDto);
  }

  @Get(':id')
  async findOne(@Param('id') id: string) {
    console.log('Find: Customers');
    console.log(id);
    return await this.customersService.findOne(id);
  }

  @Patch(':id')
  async update(
    @Param('id') id: string,
    @Body() updateCustomerDto: UpdateCustomerDto,
  ) {
    console.log('Update: Customers');
    console.log(id);
    console.log(updateCustomerDto);

    const errors = await validate(updateCustomerDto);

    if (errors.length > 0) {
      // Handle validation errors
      throw new HttpException('', 400);
    }

    if (
      updateCustomerDto.email &&
      !this.validationService.emailValidation(updateCustomerDto.email)
    ) {
      throw new HttpException('Email is not valid', 401);
    }

    return await this.customersService.update(id, updateCustomerDto);
  }

  @Delete(':id')
  @HttpCode(204)
  async remove(@Param('id') id: string) {
    console.log('Remove: Customers');
    console.log(id);
    return await this.customersService.remove(id);
  }
}
