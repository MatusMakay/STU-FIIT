# Databazove systemy zadanie 5

## Zmeny oproti pôvodnemu návrhu databazy

- borrowing som premenoval na rental
- finess neobsahuje id reminder
- vztah medzi publication a reviews som zmenil z many to many na many to one
  - viacere reviews mozu odkazovat na tu istu publikaciu
- reservations a rentals som neevidoval cez ID card ale priamo na ID usera

## Navod na spustenie

- v priecinku db/migrations sa nachadzaju migracie ktore som vygeneroval v ramci minimalnych poziadaviek
- ak ich chcete spustit pouzite prikaz `npm run migration:run`
- **Toto ale nie je potrebne nakolko mam v zdrojovom kode zapnutu synchronizaciu a tabulky sa vytvoria samy podla stavu entit a databazy**
- aplikaciu lokalne odporucam spustit v pomocou prikazu `docker compose up`

Postup:

1. vytvor docker-compose.yaml
2. vloz nasledujuci kod

```
services:
  postgres:
    image: postgres:latest
    container_name: dbs_postgres

    environment:
      - POSTGRES_HOST=postgres
      - POSTGRES_PORT=5432
      - POSTGRES_USER=passwd
      - POSTGRES_PASSWORD=passwd
      - POSTGRES_DB=db
    ports:
      - 5432:5432

  dbsapp:
    build:
      context: .
      dockerfile: ./Dockerfile

    image: mydbsapp

    environment:
      - DATABASE_HOST=postgres
      - DATABASE_PORT=5432
      - DATABASE_USER=passwd
      - DATABASE_PASSWORD=passwd
      - DATABASE_NAME=db

    depends_on:
      - postgres

    ports:
      - 8000:8000
```

3. prikazom `docker compose up` spusti aplikaciu

## Zadanie

- v dokumentacii som sa rozhodol popisat endpointy ktore mi nefungovali na testeri ale lokalne pri spustani fungovali

### Rentals

#### Post /rentals

Postup:

1. Najdem si pouzivatela a skontrolujem ci existuje ak nie vyhodim 404
2. Najdem publikaciu a skontrolujem ci existuje ak nie vyhodim 404
3. Najdem instanciu publikacie ktora ma stav available ak taku nenajdem viem ze bud neexistuje alebo nie je volna a vyhodim 400
4. Ak volna kopia existuje zmenim jej stav na nedostupny a vytvorim prislusnu vypozicku
5. Upravim response do ziadaneho stavu a vratim 201

```
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
```

#### GET /rentals/:id

1. Vyhladam publikaciu, ak neexistuje vraciam 404
2. Ak hej upravim response do ziadaneho stavu a vratim 200

```
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
```

#### PATCH /rentals/:id

1. Vyhladam vypozicku a ak neexistuje vyhodim 404
2. zmenim prislusne data
3. upravim odpoved do ziadaneho stavu a vratim 200

```
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
```

### Users

#### GET /users/:id

1. Vyhladam pouzivatela ak neexistuje vyhodim 404
2. Vyhladam pre neho jeho rezervacia a vypozicky pomocou metody findReservationsAndRentals
3. Upravim response do ziadaneho stavu a vratim 200 s objektom pricom ak su rentals prazdne nevlozim ich do odpovede

```
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
```

#### Post /users

1. Vyhladam ci existuje uz rovnaky email v databaze ak ano vyhodim Conficlict Error
2. Skontrolujem ci je email zadany v spravnej forme ak nie vyhodim 400
3.

```
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

    newCustomer.reservations = findReservations;
    newCustomer.borrowings = findRentals;

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
```
