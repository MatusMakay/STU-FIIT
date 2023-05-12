import { HttpException, Injectable, NotFoundException } from '@nestjs/common';
import { CreateCustomerDto } from './dto/create-customer.dto';
import { UpdateCustomerDto } from './dto/update-customer.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Customers } from './entities/customer.entity';
import { Repository } from 'typeorm';
import { Reservations } from '../reservations/entities/reservations.entity';
import { Rentals } from '../rentals/entities/borrowings.entity';
import { ResponseCustomerDto } from './dto/response-customer.dto';
import { validate } from 'class-validator';
import { cursorTo } from 'readline';
import { Copy } from 'src/copy/entities/copy.entity';

@Injectable()
export class CustomersService {
  constructor(
    @InjectRepository(Customers)
    private customersRepository: Repository<Customers>,
    @InjectRepository(Reservations)
    private reservationsRepository: Repository<Reservations>,
    @InjectRepository(Rentals) private rentalsRepository: Repository<Rentals>,
    @InjectRepository(Copy) private copyRepository: Repository<Copy>,
  ) {}

  isValidDate(dateString: string): boolean {
    const date: Date = new Date(dateString);
    return date instanceof Date && !isNaN(date.getTime());
  }

  async findReservationsAndRentals(customer: Customers) {
    const reservations = await this.reservationsRepository.find({
      relations: {
        customer: true,
        publication: true,
      },
      where: {
        customer: {
          id: customer.id,
        },
      },
    });

    const rentals = await this.rentalsRepository.find({
      relations: {
        customer: true,
        copy: true,
      },
      where: {
        customer: {
          id: customer.id,
        },
      },
    });

    return {
      rentals: rentals,
      reservations: reservations,
    };
  }

  async create(createCustomerDto: CreateCustomerDto) {
    const occupiedEmails = await this.customersRepository.find({
      where: {
        email: createCustomerDto.email,
      },
    });

    if (occupiedEmails.length != 0) {
      throw new HttpException(`Email already taken`, 409);
    }

    let newCustomer: Customers;
    if (this.isValidDate(createCustomerDto.birth_date)) {
      newCustomer = this.customersRepository.create(createCustomerDto);
    } else {
      throw new HttpException('Missing infromations', 400);
    }

    const { rentals: findRentals, reservations: findReservations } =
      await this.findReservationsAndRentals(newCustomer);

    // newCustomer.reservations = findReservations;
    // newCustomer.borrowings = findRentals;

    let { deleted_at, hasParent, borrowings, reservations, id_card, ...tmp } =
      await this.customersRepository.save(newCustomer);
    let response = {
      ...tmp,
      updated_at: newCustomer.updated_at,
      created_at: newCustomer.created_at,
    };
    if (findReservations.length != 0) {
      response['reservations'] = findReservations;
    }

    if (findRentals.length != 0) {
      response['rentals'] = findRentals;
    }

    return response;
  }

  async findOne(id: string) {
    let customer = await this.customersRepository.findOne({
      // relations: {
      //   borrowings: true,
      //   reservations: true,
      // },
      where: {
        id: id,
      },
    });

    if (!customer) {
      throw new NotFoundException(`User with id ${id} not found`);
    }

    const { rentals, reservations } = await this.findReservationsAndRentals(
      customer,
    );

    let { deleted_at, hasParent, id_card, borrowings, ...tmp } = customer;

    let response = {
      ...tmp,
      updated_at: customer.updated_at,
      created_at: customer.created_at,
    };

    let tmpList = [];
    if (rentals.length != 0) {
      await Promise.all(
        rentals.map(async (rental) => {
          let obj = {};
          obj['id'] = rental.id;
          obj['user_id'] = customer.id;
          obj['duration'] = rental.duration;
          obj['status'] = rental.status;
          let publication = await this.copyRepository.findOne({
            relations: {
              publication: true,
            },
            where: {
              id: rental.copy.id,
            },
          });
          obj['publication_instance_id'] = publication.id;
          tmpList.push(obj);
        }),
      );

      response['rentals'] = tmpList;
    }

    if (reservations.length != 0) {
      tmpList = [];

      reservations.forEach((reservation) => {
        let obj = {};
        obj['id'] = reservation.id;
        obj['user_id'] = customer.id;
        obj['publication_id'] = reservation.publication.id;
        tmpList.push(obj);
      });

      response['reservations'] = tmpList;
    }

    return response;
  }

  async update(id: string, updateCustomerDto: UpdateCustomerDto) {
    let customer = await this.customersRepository.findOne({
      where: {
        id: id,
      },
    });

    if (!customer) {
      throw new NotFoundException(`User with id ${id} not found`);
    }
    const { deleted_at, hasParent, created_at, updated_at, ...response } =
      await this.customersRepository.save({
        ...customer,
        ...updateCustomerDto,
      });

    return response;
  }

  remove(id: string) {
    return `This action removes a #${id} customer`;
  }

  findAll() {
    return `This action returns all customers`;
  }
}
