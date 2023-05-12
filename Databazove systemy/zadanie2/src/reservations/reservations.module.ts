import { Module } from '@nestjs/common';
import { ReservationsService } from './reservations.service';
import { ReservationsController } from './reservations.controller';
import { Reservations } from './entities/reservations.entity';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Customers } from 'src/customers/entities/customer.entity';
import { Publications } from 'src/publication/entities/publication.entity';

@Module({
  imports: [TypeOrmModule.forFeature([
    Reservations,
    Customers,
    Publications
  ])],
  controllers: [ReservationsController],
  providers: [ReservationsService]
})
export class ReservationsModule {}
