import { Module } from '@nestjs/common';
import { PublicationService } from './publication.service';
import { PublicationController } from './publication.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Publications } from './entities/publication.entity';
import { Populars } from './entities/populars.entity';
import { PublicationsWishLists } from './entities/publication-wishlists.entity';
import { Autors } from 'src/autors/entities/autor.entity';
import { Genre } from 'src/genre/entities/genre.entity';

@Module({
  imports: [TypeOrmModule.forFeature([
    Publications,
    Autors,
    Populars,
    Genre,
    PublicationsWishLists,
  ])],
  controllers: [PublicationController],
  providers: [PublicationService]
})
export class PublicationModule {}
