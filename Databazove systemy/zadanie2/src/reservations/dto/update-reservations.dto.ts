import { PartialType } from '@nestjs/mapped-types';
import { CreateReservationsDto } from './create-reservations.dto';

export class UpdateReservationsDto extends PartialType(CreateReservationsDto) {}
