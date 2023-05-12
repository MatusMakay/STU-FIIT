import {
  BadRequestException,
  ConflictException,
  HttpException,
  Injectable,
  NotFoundException,
} from '@nestjs/common';
import { CreateAutorDto } from './dto/create-autor.dto';
import { UpdateAutorDto } from './dto/update-autor.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Autors } from './entities/autor.entity';
import { Repository } from 'typeorm';
import { find } from 'rxjs';

@Injectable()
export class AutorsService {
  constructor(
    @InjectRepository(Autors) private autorRepository: Repository<Autors>,
  ) {}

  async create(createAutorDto: CreateAutorDto) {
    let findAuthor = await this.autorRepository.findOneBy({
      id: createAutorDto.id,
    });

    if (findAuthor) {
      throw new ConflictException();
    }

    const autor = this.autorRepository.create(createAutorDto);

    try {
      const { deleted_at, description, ...response } =
        await this.autorRepository.save(autor);
      return response;
    } catch (err) {
      throw new HttpException('', 400);
    }
  }

  async findOne(id: string) {
    const autor = await this.autorRepository.findOneBy({
      id: id,
    });

    if (!autor) {
      throw new NotFoundException(`Author with provided id ${id} not exists`);
    }

    const { deleted_at, ...response } = autor;
    return response;
  }

  async update(id: string, updateAutorDto: UpdateAutorDto) {
    let author = await this.autorRepository.findOneBy({
      id: id,
    });

    author.name =
      updateAutorDto.name != undefined ? updateAutorDto.name : author.name;
    author.surname =
      updateAutorDto.surname != undefined
        ? updateAutorDto.surname
        : author.surname;

    const { deleted_at, description, ...response } =
      await this.autorRepository.save(author);

    return response;
  }

  async remove(id: string) {
    let author = await this.autorRepository.findOneBy({
      id: id,
    });

    return this.autorRepository.softRemove(author);
  }
}
