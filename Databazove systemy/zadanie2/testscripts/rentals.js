const { postListOnPath } = require('./common');

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

postListOnPath(rentals, 'rentals');

// rentals.forEach(async (rental) => {
//   const response = await fetch(appurl + '/rentals', {
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
//     body: JSON.stringify(rental), // body data type must match "Content-Type" header
//   });
//   const json = await response.json();
//   console.log(response.status);
//   console.log(json);
// });
