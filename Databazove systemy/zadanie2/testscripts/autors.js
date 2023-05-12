const { postListOnPath } = require('./common');

const autors = [
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

postListOnPath(autors, 'authors');

// autors.forEach(async (autor) => {
//   const response = await fetch(appurl + '/authors', {
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
//     body: JSON.stringify(autor), // body data type must match "Content-Type" header
//   });
//   const json = await response.json();
//   console.log(response.status);
//   console.log(json);
// });
