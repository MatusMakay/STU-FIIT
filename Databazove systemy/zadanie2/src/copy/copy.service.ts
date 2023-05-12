import { Injectable, NotFoundException } from '@nestjs/common';
import { CreateCopyDto } from './dto/create-copy.dto';
import { UpdateCopyDto } from './dto/update-copy.dto';
import { Copy } from './entities/copy.entity';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Publications } from 'src/publication/entities/publication.entity';
import { ResponseCopyDto } from './dto/response-copy.dto';
import { response } from 'express';

@Injectable()
export class CopyService {
  constructor(
    @InjectRepository(Copy) private copyRepository: Repository<Copy>,
    @InjectRepository(Publications)
    private publicationsRepository: Repository<Publications>,
  ) {}

  async create(createCopyDto: CreateCopyDto) {
    let newCopy: Copy = new Copy();

    let { publication_id, ...parsedDto } = createCopyDto;

    this.copyRepository.create(parsedDto);

    const relationPub: Publications =
      await this.publicationsRepository.findOneBy({
        id: publication_id,
      });

    if (!relationPub) {
      throw new NotFoundException(
        `Publication with id ${publication_id} doesn't exists`,
      );
    }

    newCopy.publication = relationPub;
    newCopy.id = parsedDto.id;
    newCopy.publisher = parsedDto.publisher;
    newCopy.year = parsedDto.year;
    newCopy.status = parsedDto.status;
    newCopy.type = parsedDto.type;

    let { deleted_at, publication, available, ...response } =
      await this.copyRepository.save(newCopy);

    response['publication_id'] = publication.id;

    return response;
  }

  async findOne(id: string) {
    const copy = await this.copyRepository.findOne({
      relations: {
        publication: true,
      },
      where: {
        id: id,
      },
    });

    if (!copy) {
      throw new NotFoundException(`Copy with id ${id} not found`);
    }

    let { deleted_at, publication, available, ...response } = copy;

    response['publication_id'] = publication.id;

    return response;
  }

  async update(id: string, updateCopyDto: UpdateCopyDto) {
    let copy = await this.copyRepository.findOne({
      relations: {
        publication: true,
      },
      where: {
        id: id,
      },
    });

    if (!copy) {
      throw new NotFoundException(`Copy with id ${id} not found`);
    }

    copy.type =
      updateCopyDto.type != undefined ? updateCopyDto.type : copy.type;
    copy.publisher =
      updateCopyDto.publisher != undefined
        ? updateCopyDto.publisher
        : copy.publisher;
    copy.year =
      updateCopyDto.year != undefined ? updateCopyDto.year : copy.year;
    copy.status =
      updateCopyDto.status != undefined ? updateCopyDto.status : copy.status;

    const relationPub: Publications =
      await this.publicationsRepository.findOneBy({
        id: updateCopyDto.publication_id,
      });

    if (!relationPub) {
      throw new NotFoundException(
        `Publication with id ${updateCopyDto.publication_id} doesn't exists`,
      );
    }

    copy.publication = relationPub;

    let { deleted_at, publication, available, ...response } =
      await this.copyRepository.save(copy);

    response['publication_id'] = publication.id;

    return response;
  }

  async remove(id: string) {
    const copy = await this.copyRepository.findOne({
      relations: {
        publication: true,
      },
      where: {
        id: id,
      },
    });

    if (!copy) {
      throw new NotFoundException(`Copy with id ${id} not found`);
    }

    this.copyRepository.softRemove(copy);

    return {};
  }
}
