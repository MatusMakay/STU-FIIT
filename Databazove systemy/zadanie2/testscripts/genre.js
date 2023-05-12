const { postListOnPath } = require('./common');

const genre = [
  { id: '2536043f-9965-48d0-9fe3-09861f6b96d8', name: 'Fiction' },
  { id: '2e6d6949-e880-43ba-bd03-349b4bd544d2', name: 'Poetry' },
  { id: '6e26d6e7-6c2e-4177-86f1-39274c524399', name: 'Thriller' },
  { id: 'b0c3b9be-bf50-42ab-bb0b-a6cecf0e9946', name: 'Fantasy' },
  { id: 'f614e379-05a1-41b5-b598-96c84af3ccd5', name: 'Children' },
];

postListOnPath(genre, 'categories');

// genre.forEach(async (specgenre) => {
//   const response = await fetch(appurl + '/categories', {
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
//     body: JSON.stringify(specgenre), // body data type must match "Content-Type" header
//   });
//   const json = await response.json();
//   console.log(response.status);
//   console.log(json);
// });
