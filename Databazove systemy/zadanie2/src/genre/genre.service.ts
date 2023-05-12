import {
  BadRequestException,
  ConflictException,
  HttpException,
  Injectable,
  NotFoundException,
} from '@nestjs/common';
import { CreateGenreDto } from './dto/create-genre.dto';
import { UpdateGenreDto } from './dto/update-genre.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Genre } from './entities/genre.entity';
import { Not, Repository } from 'typeorm';
import { response } from 'express';

@Injectable()
export class GenreService {
  constructor(
    @InjectRepository(Genre) private genreRepository: Repository<Genre>,
  ) {}

  async create(createGenreDto: CreateGenreDto) {
    const genre = this.genreRepository.create(createGenreDto);

    const { deleted_at, ...response } = await this.genreRepository.save(genre);

    return response;
  }

  async findOne(id: string) {
    const genre = await this.genreRepository.findOneBy({ id: id });

    if (!genre) {
      throw new HttpException('not found', 404);
    }

    return genre;
  }

  async update(id: string, updateGenreDto: UpdateGenreDto) {
    let genre = await this.genreRepository.findOneBy({ id: id });

    // const check: number = Number(updateGenreDto.name); // Convert string1 to a number

    // if (!isNaN(check)) {
    //   throw new BadRequestException();
    // }
    if (typeof updateGenreDto.name !== 'string') {
      throw new BadRequestException('not a string');
    }

    if (!genre) {
      throw new NotFoundException('not found');
    }
    genre.name = updateGenreDto.name;

    const { deleted_at, ...response } = await this.genreRepository.save(genre);

    return response;
  }

  async remove(id: string) {
    let genre = await this.genreRepository.findOneBy({
      id: id,
    });
    if (!genre) {
      throw new NotFoundException('not found');
    }
    return await this.genreRepository.softRemove(genre);
  }
}
