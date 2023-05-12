import { Injectable, NotFoundException } from '@nestjs/common';
import { CreateReservationsDto } from './dto/create-reservations.dto';
import { UpdateReservationsDto } from './dto/update-reservations.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Publications } from 'src/publication/entities/publication.entity';
import { Reservations } from './entities/reservations.entity';
import { Customers } from 'src/customers/entities/customer.entity';
import { response } from 'express';

@Injectable()
export class ReservationsService {
  constructor(
    @InjectRepository(Customers)
    private customerRepository: Repository<Customers>,
    @InjectRepository(Publications)
    private publicationRepository: Repository<Publications>,
    @InjectRepository(Reservations)
    private reservationRepository: Repository<Reservations>,
  ) {}

  async create(createReservationDto: CreateReservationsDto) {
    const { user_id, publication_id, id } = createReservationDto;

    let findCustomer = await this.customerRepository.findOne({
      relations: {
        reservations: true,
      },
      where: {
        id: user_id,
      },
    });

    if (!findCustomer) {
      throw new NotFoundException(
        `User with provided id ${user_id} doesn't exists`,
      );
    }

    const findPublication = await this.publicationRepository.findOneBy({
      id: publication_id,
    });

    if (!findPublication) {
      throw new NotFoundException(
        `Publications with provided id ${publication_id} doesn't exists`,
      );
    }

    let reservation = await this.reservationRepository.create({ id });

    reservation.customer = findCustomer;
    reservation.publication = findPublication;
    findCustomer.reservations.push(reservation);
    this.customerRepository.save(findCustomer);
    const {
      state,
      expired,
      everything_returned,
      pick_up_date,
      deleted_at,
      updated_at,
      customer,
      publication,
      ...response
    } = await this.reservationRepository.save(reservation);

    response['user_id'] = customer.id;
    response['publication_id'] = publication.id;

    return response;
  }

  async findOne(id: string) {
    const reservation = await this.reservationRepository.findOne({
      relations: {
        publication: true,
        customer: true,
      },
      where: {
        id: id,
      },
    });

    if (!reservation) {
      throw new NotFoundException(
        `Reservation with provided id ${id} doesn't exists`,
      );
    }

    const {
      state,
      expired,
      everything_returned,
      pick_up_date,
      deleted_at,
      updated_at,
      customer,
      publication,
      ...response
    } = await this.reservationRepository.save(reservation);

    response['user_id'] = customer.id;
    response['publication_id'] = publication.id;

    return response;
  }

  async remove(id: string) {
    const reservation = await this.reservationRepository.findOne({
      relations: {
        publication: true,
        customer: true,
      },
      where: {
        id: id,
      },
    });

    if (!reservation) {
      throw new NotFoundException(
        `Reservation with provided id ${id} doesn't exists`,
      );
    }

    this.reservationRepository.softRemove(reservation);
    return {};
  }
}
