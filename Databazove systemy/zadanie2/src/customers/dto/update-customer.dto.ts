import { PartialType } from '@nestjs/mapped-types';
import { CreateCustomerDto } from './create-customer.dto';
import { IsDate, IsString } from 'class-validator';

export class UpdateCustomerDto extends PartialType(CreateCustomerDto) {
  @IsString()
  name?: string;
  @IsString()
  surname?: string;
  @IsString()
  email?: string;
  @IsString()
  birth_date?: string;
  @IsString()
  personal_identificator?: string;
}
