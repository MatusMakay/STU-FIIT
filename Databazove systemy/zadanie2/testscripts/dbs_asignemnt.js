const {
  postListOnPath,
  getEntity,
  deleteEntity,
  updateEntity,
} = require('./common');

const users = [
  {
    id: 'f191a098-9d32-4de0-9993-97a5acdd6f6f',
    name: 'Minato',
    email: '4th-hokage@konoha.leaf',
    surname: 'Namikaze',
    birth_date: '1955-01-10',
    personal_identificator: '2223331111',
  },
  {
    id: '082613a0-e607-11ed-a05b-0242ac120003',
    name: 'Jozef',
    email: 'jozef.dent@dbs.com',
    surname: 'Dent',
    birth_date: '2000-08-24',
    personal_identificator: '12345678',
  },
  {
    id: '7b664872-7511-4ae4-9e95-6bb24715991e',
    name: 'Frodo',
    email: 'bublikovci@hobit.com',
    surname: 'Bublik',
    birth_date: '1996-01-05',
    personal_identificator: '5566778765',
  },
];

postListOnPath(users, 'users');
const f1Customer = '082613a0-e607-11ed-a05b-0242ac120003';
getEntity(f1Customer, 'users');

const upd1CustId = '082613a0-e607-11ed-a05b-0242ac120003';
const upda1Cust = { surname: 'Dentist' };
updateEntity(upd1CustId, upda1Cust, 'users');

const f2Customer = '082613a0-e607-11ed-a05b-0242ac120003';
getEntity(f2Customer, 'users');

const authors = [
  {
    id: '056b8743-c745-4009-9fae-1ad1ce6b36a0',
    name: 'Samo',
    surname: 'Chalupka',
  },
  {
    id: 'd46b8ccb-c7c6-42f2-94c2-a5219402b442',
    name: 'J.R.R.',
    surname: 'Tolkien',
  },
  {
    id: 'f62752a5-834c-4af4-837b-0921431764c2',
    name: 'William',
    surname: 'Shakespeare',
  },
  {
    id: 'bd74d3b7-177a-4031-9bf5-79053bf98ba4',
    name: 'J.K.',
    surname: 'Rowling',
  },
  {
    id: '50ecc2d4-7d8c-4150-821a-9515b72b36cd',
    name: 'Dan',
    surname: 'Brown',
  },
];

postListOnPath(authors, 'authors');

const pub = [
  {
    id: '0161808e-1060-4ff7-bd9f-3540341174af',
    title: 'Mor ho!',
    authors: [{ name: 'Samo', surname: 'Chalupka' }],
    categories: ['Poetry', 'Thriller'],
  },
  {
    id: 'c82ba979-a49b-4342-9fec-e2da9162bb8d',
    title: 'Mor ho!',
    authors: [{ name: 'Ľudovít', surname: 'Štúr' }],
    categories: ['Poetry', 'Thriller'],
  },
  {
    id: 'aa61808e-1060-4ff7-bd9f-354034117400',
    title: 'Branko',
    authors: [
      { name: 'Samo', surname: 'Chalupka' },
      { name: 'Dan', surname: 'Brown' },
    ],
    categories: ['Poetry', 'Fantasy'],
  },
];

postListOnPath(pub, 'publications');

const pubf1 = '0161808e-1060-4ff7-bd9f-3540341174af';
getEntity(pubf1, 'publications');

const pubf2 = 'c82ba979-a49b-4342-9fec-e2da9162bb8d';
getEntity(pubf2, 'publications');

const copys = [
  {
    id: '0b7a84db-a097-4c22-94e6-8df6df9e968f',
    type: 'physical',
    year: 1949,
    status: 'available',
    publisher: 'Penguin Books',
    publication_id: '0161808e-1060-4ff7-bd9f-3540341174af',
  },
  {
    id: '0b7a84db-a097-4c22-94e6-8df6df9effff',
    type: 'physical',
    year: 1950,
    status: 'available',
    publisher: 'Asd Books',
    publication_id: 'aa61808e-1060-4ff7-bd9f-354034117400',
  },
];

const copyf1 = '0b7a84db-a097-4c22-94e6-8df6df9e968f';

postListOnPath(copys, 'instances');
getEntity(copyf1, 'instances');

const rentals = [
  {
    id: '6c3263d5-f8c2-4488-86a1-59c090316e02',
    user_id: 'f191a098-9d32-4de0-9993-97a5acdd6f6f',
    duration: 14,
    publication_id: '0161808e-1060-4ff7-bd9f-3540341174af',
  },
  {
    id: '6c3263d5-f8c2-4488-86a1-59c090316a55',
    user_id: '7b664872-7511-4ae4-9e95-6bb24715991e',
    duration: 14,
    publication_id: '0161808e-1060-4ff7-bd9f-3540341174af',
  },

  {
    id: '3049dd83-bea5-4526-880b-d55076107cc8',
    user_id: '7b664872-7511-4ae4-9e95-6bb24715991e',
    duration: 14,
    publication_id: '0161808e-1060-4ff7-bd9f-3540341174af',
  },
  {
    id: 'd1d032d1-a033-4bca-91c7-6ce5579fcbd7',
    user_id: '082613a0-e607-11ed-a05b-0242ac120003',
    duration: 14,
    publication_id: '0161808e-1060-4ff7-bd9f-3540341174af',
  },
  {
    id: 'd1d032d1-a033-0000-91c7-6ce5579fcb00',
    user_id: '082613a0-e607-11ed-a05b-0242ac120003',
    duration: 5,
    publication_id: 'aa61808e-1060-4ff7-bd9f-354034117400',
  },
];

const reservations = [
  {
    id: 'afc217ff-fd6f-4c62-8072-d361d140fcbe',
    user_id: '082613a0-e607-11ed-a05b-0242ac120003',
    publication_id: '0161808e-1060-4ff7-bd9f-3540341174af',
  },
  {
    id: '36c2ddac-48eb-42e3-9e48-f2f16797da01',
    user_id: '7b664872-7511-4ae4-9e95-6bb24715991e',
    publication_id: '0161808e-1060-4ff7-bd9f-3540341174af',
  },
];

postListOnPath(rentals, 'rentals');
postListOnPath(reservations, 'reservations');

const copyUpd1Id = '0b7a84db-a097-4c22-94e6-8df6df9e968f';
const copyUpd1 = { status: 'available' };
updateEntity(copyUpd1Id, copyUpd1, 'instances');

const custUpd1Id = '082613a0-e607-11ed-a05b-0242ac120003';
const custUpd1 = { surname: 'Dent' };
updateEntity(custUpd1Id, custUpd1, 'users');

const custf1 = '082613a0-e607-11ed-a05b-0242ac120003';
getEntity(custf1, 'users');

const authorsv2 = [
  {
    id: '497f6eca-6276-4993-bfeb-53cbbbba6f08',
    name: 'Ernest',
    surname: 'Hemingway',
  },
  {
    id: '497f6eca-6276-4993-bfeb-53cbbbba6f08',
    name: 'Jozef',
    surname: 'Mak',
  },
  { id: '497f6eca-6276-4993-bfeb-53cbbbba6f00', name: 'Jan' },
];

postListOnPath(authorsv2, 'authors');

const authorsf2 = '497f6eca-6276-4993-bfeb-53cbbbba6f08';
getEntity(authorsf2, 'authors');

const authorsf3 = '000f6eca-6276-4993-bfeb-53cbbbba6f08';
getEntity(authorsf3, 'authors');

const authorsUpd1Id = '497f6eca-6276-4993-bfeb-53cbbbba6f08';
const authorsUpd1 = { name: 'Jan' };
updateEntity(authorsUpd1Id, authorsUpd1, 'authors');

const authorsf4 = '497f6eca-6276-4993-bfeb-53cbbbba6f08';
getEntity(authorsf4, 'authors');

const genres = [
  { id: '2536043f-9965-48d0-9fe3-09861f6b96d8', name: 'Fiction' },
  { id: '2e6d6949-e880-43ba-bd03-349b4bd544d2', name: 'Poetry' },
  { id: '6e26d6e7-6c2e-4177-86f1-39274c524399', name: 'Thriller' },
  { id: 'b0c3b9be-bf50-42ab-bb0b-a6cecf0e9946', name: 'Fantasy' },
  { id: 'f614e379-05a1-41b5-b598-96c84af3ccd5', name: 'Children' },
];

postListOnPath(genres, 'categories');

const genref1 = '6e26d6e7-6c2e-4177-86f1-39274c524399';
getEntity(genref1, 'categories');
const genreUpd1Id = '0006d6e7-6c2e-4177-86f1-39274c524399';
const genreUpd1 = { name: 'scifi' };
updateEntity(genreUpd1Id, genreUpd1, 'categories');

const genreUpd2Id = '0006d6e7-6c2e-4177-86f1-39274c524399';
const genreUpd2 = { name: 2000 };
updateEntity(genreUpd2Id, genreUpd2, 'categories');

const genreUpd3Id = '6e26d6e7-6c2e-4177-86f1-39274c524399';
const genreUpd3 = { name: 'scifi' };
updateEntity(genreUpd3Id, genreUpd3, 'categories');

const genref2 = '0000d6e7-6c2e-4177-86f1-39274c524399';
getEntity(genref2, 'categories');

const genref3 = '6e26d6e7-6c2e-4177-86f1-39274c524399';
getEntity(genref3, 'categories');

const rmGenre = '6e26d6e7-6c2e-4177-86f1-39274c524399';
deleteEntity(rmGenre, 'categories');

const genref4 = '6e26d6e7-6c2e-4177-86f1-39274c524399';
getEntity(genref4, 'categories');

const cards = [
  {
    id: 'c82ba979-a49b-4342-9fec-e2da9162bb8d',
    status: 'active',
    user_id: '082613a0-e607-11ed-a05b-0242ac120003',
    magstripe: 'SuperSecret123456789',
  },
];
postListOnPath(cards, 'cards');

const cardsf1 = 'c82ba979-a49b-4342-9fec-e2da9162bb8d';
getEntity(cardsf1, 'cards');

const cardsUpd1Id = 'c82ba979-a49b-4342-9fec-e2da9162bb8d';
const cardsUpd1 = { status: 'pokazena' };
updateEntity(cardsUpd1Id, cardsUpd1, 'cards');

const cardsf2 = 'c82ba979-a49b-4342-9fec-e2da91620000';
getEntity(cardsf2, 'cards');

const cardsUpd2Id = 'c82ba979-a49b-4342-9fec-e2da9162bb8d';
const cardsUpd2 = { status: 'inactive' };
updateEntity(cardsUpd2Id, cardsUpd2, 'cards');

const cardsUpd3Id = 'c82ba979-a49b-4342-9fec-e2da9162bb8d';
const cardsUpd3 = { status: 'expired' };
updateEntity(cardsUpd3Id, cardsUpd3, 'cards');

const cardsf3 = 'c82ba979-a49b-4342-9fec-e2da9162bb8d';
getEntity(cardsf3, 'cards');

const cardsRm1 = '002ba979-a49b-4342-9fec-e2da9162bb8d';
deleteEntity(cardsRm1, 'cards');

const cardsRm2 = 'c82ba979-a49b-4342-9fec-e2da9162bb8d';
deleteEntity(cardsRm2, 'cards');

const cardsf4 = 'c82ba979-a49b-4342-9fec-e2da9162bb8d';
getEntity(cardsf4, 'cards');

const authorRm1 = '497f6eca-6276-4993-bfeb-53cbbbba6f08';
deleteEntity(authorRm1, 'authors');

const authorsf1 = '497f6eca-6276-4993-bfeb-53cbbbba6f08';
getEntity(authorsf1, 'authors');

const customersv2 = [
  {
    id: '082613a0-e607-11ed-a05b-0242ac120024',
    name: 'František',
    email: 'frantisek@dbs.com',
    surname: 'asdqsad',
    birth_date: 'tisicdevestvo',
    personal_identificator: '12345624',
  },
  {
    id: '082613a0-e607-11ed-a05b-0242ac120023',
    name: 'František',
    surname: 'Chybajucimail',
    birth_date: '1990-08-24',
    personal_identificator: '12345623',
  },
  {
    id: '082613a0-e607-11ed-a05b-0242ac120022',
    name: 'František',
    email: 'jozef.dent@dbs.com',
    surname: 'Duplicitny',
    birth_date: '1990-08-24',
    personal_identificator: '12345622',
  },
];

postListOnPath(customersv2, 'users');

getEntity('082613a0-e607-11ed-a05b-0242ac120003', 'users');
