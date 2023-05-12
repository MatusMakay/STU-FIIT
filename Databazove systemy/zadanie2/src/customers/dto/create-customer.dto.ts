import { IsDate, IsString } from 'class-validator';

export class CreateCustomerDto {
  @IsString()
  id: string;
  @IsString()
  name: string;
  @IsString()
  surname: string;
  @IsString()
  email: string;
  @IsString()
  birth_date: string;
  @IsString()
  personal_identificator: string;
}
