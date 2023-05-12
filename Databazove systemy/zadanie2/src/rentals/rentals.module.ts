import { Module } from '@nestjs/common';
import { RentalsService } from './rentals.service';
import { RentalsController } from './rentals.controller';
import { Rentals } from './entities/borrowings.entity';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Customers } from 'src/customers/entities/customer.entity';
import { Publications } from 'src/publication/entities/publication.entity';
import { Copy } from 'src/copy/entities/copy.entity';

@Module({
  imports: [TypeOrmModule.forFeature([
    Rentals,
    Copy,
    Customers,
    Publications
  ])],
  controllers: [RentalsController],
  providers: [RentalsService]
})
export class RentalsModule {}
