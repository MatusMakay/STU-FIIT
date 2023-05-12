const { postListOnPath } = require('./common');

const customers = [
  //   {
  //     id: 'f191a098-9d32-4de0-9993-97a5acdd6f6f',
  //     name: 'Minato',
  //     email: '4th-hokage@konoha.leaf',
  //     surname: 'Namikaze',
  //     birth_date: '1955-01-10',
  //     personal_identificator: '2223331111',
  //   },
  //   {
  //     id: '082613a0-e607-11ed-a05b-0242ac120003',
  //     name: 'Jozef',
  //     email: 'jozef.dent@dbs.com',
  //     surname: 'Dent',
  //     birth_date: '2000-08-24',
  //     personal_identificator: '12345678',
  //   },
  //   {
  //     id: '7b664872-7511-4ae4-9e95-6bb24715991e',
  //     name: 'Frodo',
  //     email: 'bublikovci@hobit.com',
  //     surname: 'Bublik',
  //     birth_date: '1996-01-05',
  //     personal_identificator: '5566778765',
  //   },
  {
    id: '082613a0-e607-11ed-a05b-0242ac120024',
    name: 'František',
    email: 'frantisek@dbs.com',
    surname: 'asdqsad',
    birth_date: 'tisicdevestvo',
    personal_identificator: '12345624',
  },
];

postListOnPath(customers, 'users');
