/** @type {import('tailwindcss').Config} */
module.exports = {
  content:
    [
      './views/index.html',
      './views/**/*.{html,js}',
    ],

  theme: {
    extend: {},

    screens: {
      'sm': {'min': '350px', 'max': '720px'},
      // => @media (min-width: 640px and max-width: 767px) { ... }

      'md': {'min': '721px', 'max': '1023px'},
      // => @media (min-width: 768px and max-width: 1023px) { ... }

      'lg': {'min': '1024px', 'max': '1279px'}
      // => @media (min-width: 1024px and max-width: 1279px) { ... }
    },
    colors:
        {
        'orange1': '#DB7210',
        'black1': '#070606',
        },

    gridTemplateRows:
        {
          '[auto,auto,1fr]': 'auto auto 1fr',
        },
  },
  plugins: [
    // ...
    require('@tailwindcss/forms'),
    require('@tailwindcss/aspect-ratio'),
  ],
}

