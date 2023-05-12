import { Module } from '@nestjs/common';
import { CustomersService } from './customers.service';
import { CustomersController } from './customers.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Cards } from './entities/cards.entity';
import { Customers } from './entities/customer.entity';
import { ReadingLists } from './entities/reading-lists.entity';
import { Reviews } from './entities/reviews.entity';
import { ValidateService } from './validation.service';
import { ReservationsModule } from 'src/reservations/reservations.module';
import { AutorsModule } from 'src/autors/autors.module';
import { CopyModule } from 'src/copy/copy.module';
import { GenreModule } from 'src/genre/genre.module';
import { PublicationModule } from 'src/publication/publication.module';
import { RentalsModule } from 'src/rentals/rentals.module';
import { Reservations } from 'src/reservations/entities/reservations.entity';
import { Rentals } from 'src/rentals/entities/borrowings.entity';
import { CardsController } from './cards.controller';
import { CardsService } from './cards.service';
import { Copy } from 'src/copy/entities/copy.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      Customers,
      Rentals,
      Reservations,
      ReadingLists,
      Cards,
      Reviews,
      Copy,
    ]),
  ],
  controllers: [CustomersController, CardsController],
  providers: [CustomersService, ValidateService, CardsService],
})
export class CustomersModule {}
