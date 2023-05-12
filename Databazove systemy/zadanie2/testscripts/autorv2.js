const { postListOnPath } = require('./common');

const authors = [
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

postListOnPath(authors, 'authors');
