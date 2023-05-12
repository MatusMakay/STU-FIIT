const { postListOnPath } = require('./common');

const publications = [
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

postListOnPath(publications, 'publications');

// publications.forEach(async (publication) => {
//   const response = await fetch(appurl + '/publications', {
//     method: 'POST', // *GET, POST, PUT, DELETE, etc.
//     mode: 'cors', // no-cors, *cors, same-origin
//     cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
//     credentials: 'same-origin', // include, *same-origin, omit
//     headers: {
//       'Content-Type': 'application/json',
//       // 'Content-Type': 'application/x-www-form-urlencoded',
//     },
//     redirect: 'follow', // manual, *follow, error
//     referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
//     body: JSON.stringify(publication), // body data type must match "Content-Type" header
//   });
//   const json = await response.json();
//   console.log(response.status);
//   console.log(json);
// });
