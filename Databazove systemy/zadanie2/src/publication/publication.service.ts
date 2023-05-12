import {
  BadRequestException,
  Injectable,
  NotFoundException,
} from '@nestjs/common';
import { CreatePublicationDto } from './dto/create-publication.dto';
import { UpdatePublicationDto } from './dto/update-publication.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Publications } from './entities/publication.entity';
import { Repository } from 'typeorm';
import { Autors } from 'src/autors/entities/autor.entity';
import { Genre } from 'src/genre/entities/genre.entity';
import { ResponsePublicationDto } from './dto/response-publication.dto';
import { find } from 'rxjs';

@Injectable()
export class PublicationService {
  constructor(
    @InjectRepository(Publications)
    private publicationRepository: Repository<Publications>,
    @InjectRepository(Autors) private autorRepository: Repository<Autors>,
    @InjectRepository(Genre) private genreRepository: Repository<Genre>,
  ) {}

  async findCategories(dto: CreatePublicationDto | UpdatePublicationDto) {
    let genre: Genre[] = [];

    await Promise.all(
      dto.categories.map(async (category) => {
        genre.push(
          await this.genreRepository.findOne({
            where: {
              name: category,
            },
          }),
        );
      }),
    );
    return genre;
  }

  async findAuthors(dto: CreatePublicationDto | UpdatePublicationDto) {
    let authors: Autors[] = [];

    await Promise.all(
      dto.authors.map(async (author) => {
        authors.push(
          await this.autorRepository.findOne({
            where: {
              name: author['name'],
              surname: author['surname'],
            },
          }),
        );
      }),
    );
    if (authors[0] == null) return null;

    return authors;
  }

  async findPublication(id: string): Promise<Publications> {
    const publication = await this.publicationRepository.findOne({
      relations: {
        authors: true,
        categories: true,
      },

      where: {
        id: id,
      },
    });

    if (!publication) {
      throw new NotFoundException(
        `Publication with provided id ${id} doesn't exists`,
      );
    }

    return publication;
  }

  async create(createPublicationDto: CreatePublicationDto) {
    let publication: Publications = new Publications();

    publication.authors = await this.findAuthors(createPublicationDto);
    if (!publication.authors || publication.authors == null) {
      throw new BadRequestException(
        "Autors of that book doesn't exists in database",
      );
    }

    publication.categories = await this.findCategories(createPublicationDto);
    if (!publication.categories || publication.categories == null) {
      throw new NotFoundException(
        "Genre of that book doesn't exists in database",
      );
    }

    publication.title = createPublicationDto.title;
    publication.id = createPublicationDto.id;

    const {
      deleted_at,
      pages,
      available,
      type,
      categories,
      authors,
      ...response
    } = await this.publicationRepository.save(publication);

    let tmpList = [];
    authors.forEach((author) => {
      tmpList.push({ name: author.name, surname: author.surname });
    });

    response['authors'] = tmpList;
    tmpList = [];

    categories.forEach((category) => {
      tmpList.push(category.name);
    });
    response['categories'] = tmpList;

    return response;
  }

  async findOne(id: string) {
    const publication = await this.findPublication(id);

    if (!publication) {
      throw new NotFoundException(
        `Publication with provided id ${id} doesn't exists`,
      );
    }

    const {
      deleted_at,
      pages,
      available,
      type,
      categories,
      authors,
      ...response
    } = await this.publicationRepository.save(publication);

    let tmpList = [];
    authors.forEach((author) => {
      tmpList.push({ name: author.name, surname: author.surname });
    });

    response['authors'] = tmpList;
    tmpList = [];

    categories.forEach((category) => {
      tmpList.push(category.name);
    });
    response['categories'] = tmpList;

    return response;
  }

  async update(id: string, updatePublicationDto: UpdatePublicationDto) {
    let publication: Publications = await this.findPublication(id);

    if (updatePublicationDto.title) {
      publication.title = updatePublicationDto.title;
    }

    if (updatePublicationDto.authors) {
      publication.authors = await this.findAuthors(updatePublicationDto);
    }

    if (updatePublicationDto.categories) {
      publication.categories = await this.findCategories(updatePublicationDto);
    }

    const {
      deleted_at,
      pages,
      available,
      type,
      categories,
      authors,
      ...response
    } = await this.publicationRepository.save(publication);

    let tmpList = [];
    authors.forEach((author) => {
      tmpList.push({ name: author.name, surname: author.surname });
    });

    response['authors'] = tmpList;
    tmpList = [];

    categories.forEach((category) => {
      tmpList.push(category.name);
    });
    response['categories'] = tmpList;

    return response;
  }

  async remove(id: string) {
    let publication: Publications = await this.findPublication(id);

    this.publicationRepository.remove(publication);
  }
}
