import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { PublicationModule } from './publication/publication.module';
import { CustomersModule } from './customers/customers.module';
import { AutorsModule } from './autors/autors.module';
import { CopyModule } from './copy/copy.module';
import { GenreModule } from './genre/genre.module';
import { RentalsModule } from './rentals/rentals.module';
import { ReservationsModule } from './reservations/reservations.module';
import { PostgresModule } from './postgres/postgres.module';
import { ConfigModule } from '@nestjs/config';
import { LibraryModule } from './library/library.module';

@Module({
  imports: [
    //   TypeOrmModule.forRoot(
    //   dataSourceOptions
    // ),
    ConfigModule.forRoot({
      isGlobal: true,
      envFilePath: `${process.cwd()}/environments/${process.env.NODE_ENV}.env`,
    }),

    AutorsModule,
    PublicationModule,
    CustomersModule,
    CopyModule,
    GenreModule,
    RentalsModule,
    ReservationsModule,
    PostgresModule,
    LibraryModule,
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
