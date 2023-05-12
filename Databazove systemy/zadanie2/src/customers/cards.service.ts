import { Repository } from 'typeorm';
import { CreateCardsDto } from './dto/create-cards.dto';
import { UpdateCardsDto } from './dto/update-cards.dto';
import {
  BadRequestException,
  HttpException,
  Injectable,
  NotFoundException,
} from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Cards } from './entities/cards.entity';
import { Customers } from './entities/customer.entity';
import { response } from 'express';

@Injectable()
export class CardsService {
  allowedStatuses = ['active', 'expired', 'inactive'];
  constructor(
    @InjectRepository(Cards) private cardsRepository: Repository<Cards>,
    @InjectRepository(Customers)
    private customerRepository: Repository<Customers>,
  ) {}

  async create(createCardsDto: CreateCardsDto) {
    if (!this.allowedStatuses.includes(createCardsDto.status)) {
      throw new BadRequestException('');
    }

    const newUser = await this.customerRepository.findOneBy({
      id: createCardsDto.user_id,
    });

    if (!newUser) {
      throw new NotFoundException(
        `User with id ${createCardsDto.id} not found`,
      );
    }

    let { user_id, ...refDto } = createCardsDto;

    refDto['user'] = newUser;

    const newIdCard = this.cardsRepository.create(refDto);

    let { user, deleted_at, ...response } = await this.cardsRepository.save(
      newIdCard,
    );

    response['user_id'] = createCardsDto.user_id;

    return response;
  }

  async findOne(id: string) {
    const card = await this.cardsRepository.findOne({
      relations: {
        user: true,
      },
      where: {
        id: id,
      },
    });

    if (!card) {
      throw new NotFoundException(`Card with id ${id} not found`);
    }

    let { deleted_at, user, ...response } = card;

    response['user_id'] = user.id;

    return await response;
  }

  async update(id: string, updateCardDto: UpdateCardsDto) {
    // if(!updateCardDto.status || !updateCardDto.user_id){
    //   throw new HttpException("", 400)
    // }

    if (!this.allowedStatuses.includes(updateCardDto.status)) {
      throw new BadRequestException('');
    }
    const card = await this.cardsRepository.findOne({
      relations: {
        user: true,
      },
      where: {
        id: id,
      },
    });

    if (!card) {
      throw new NotFoundException(`Card with id ${id} not found`);
    }

    card.status = updateCardDto.status;

    let { deleted_at, user, ...response } = await this.cardsRepository.save(
      card,
    );

    response['user_id'] = card.user.id;

    return response;
  }

  async remove(id: string) {
    const card = await this.cardsRepository.findOne({
      where: {
        id: id,
      },
    });

    if (!card) {
      throw new NotFoundException();
    }

    return this.cardsRepository.softRemove(card);
  }
}
