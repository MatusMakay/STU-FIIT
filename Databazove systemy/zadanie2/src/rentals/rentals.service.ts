import {
  BadRequestException,
  ConflictException,
  Injectable,
  NotFoundException,
} from '@nestjs/common';
import { CreateRentalsDto } from './dto/create-rentals.dto';
import { UpdateRentalsDto } from './dto/update-rentals.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Rentals } from './entities/borrowings.entity';
import { Repository } from 'typeorm';
import { Publications } from 'src/publication/entities/publication.entity';
import { Customers } from 'src/customers/entities/customer.entity';
import { Copy } from 'src/copy/entities/copy.entity';
import { find } from 'rxjs';

@Injectable()
export class RentalsService {
  constructor(
    @InjectRepository(Rentals) private rentalsRepository: Repository<Rentals>,
    @InjectRepository(Publications)
    private publicationsRepository: Repository<Publications>,
    @InjectRepository(Customers)
    private customersRepository: Repository<Customers>,
    @InjectRepository(Copy) private copyRepository: Repository<Copy>,
  ) {}

  addDays(date, days) {
    date.setDate(date.getDate() + days);
    return date;
  }

  async create(createRentalDto: CreateRentalsDto) {
    const { user_id, publication_id, ...refDto } = createRentalDto;

    let user = await this.customersRepository.findOne({
      relations: {
        borrowings: true,
      },
      where: {
        id: user_id,
      },
    });

    if (!user) {
      throw new NotFoundException(
        `User with provided id ${user_id} doesn't exists`,
      );
    }

    const findPublication = await this.publicationsRepository.findOneBy({
      id: publication_id,
    });

    if (!findPublication) {
      throw new NotFoundException(
        `Publication with provided id ${publication_id} doesn't exists`,
      );
    }

    let findCopy = await this.copyRepository.findOne({
      relations: {
        publication: true,
      },
      where: {
        publication: {
          id: findPublication.id,
        },
        status: 'available',
      },
    });

    if (!findCopy) {
      throw new BadRequestException(`There are no free copys for publication`);
    }

    findCopy.status = 'unavailable';
    this.copyRepository.save(findCopy);

    let rental = new Rentals();
    user.borrowings.push(rental);
    this.customersRepository.save(user);
    rental.status = 'active';
    rental.id = refDto.id;
    rental.duration = refDto.duration;
    rental.customer = user;
    rental.copy = findCopy;
    rental.start_date = new Date();
    rental.end_date = this.addDays(new Date(), createRentalDto.duration);

    let { deleted_at, created_at, updated_at, customer, copy, ...response } =
      await this.rentalsRepository.save(rental);

    response['publication_instance_id'] = copy.id;
    response['user_id'] = customer.id;

    return response;
  }

  async findOne(id: string) {
    const rental = await this.rentalsRepository.findOne({
      relations: {
        copy: true,
        customer: true,
      },
      where: {
        id: id,
      },
    });

    if (!rental) {
      throw new NotFoundException(
        `Rental with provided id ${id} doesn't exists`,
      );
    }

    let { deleted_at, updated_at, created_at, customer, copy, ...response } =
      rental;

    response['publication_instance_id'] = copy.id;
    response['user_id'] = customer.id;

    return response;
  }

  async update(id: string, updateRentalDto: UpdateRentalsDto) {
    let rental = await this.rentalsRepository.findOne({
      relations: {
        copy: true,
        customer: true,
      },
      where: {
        id: id,
      },
    });

    if (!rental) {
      throw new NotFoundException('');
    }

    rental.duration = updateRentalDto.duration;
    rental.end_date = this.addDays(
      new Date(rental.start_date),
      updateRentalDto.duration,
    );

    let { deleted_at, updated_at, created_at, customer, copy, ...response } =
      await this.rentalsRepository.save(rental);

    response['publication_instance_id'] = copy.id;
    response['user_id'] = customer.id;

    return response;
  }
}
