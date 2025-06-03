/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./src/**/*.{html,ts}",
    ],
    theme: {
        extend: {
            backgroundColor: {
                'main-color': 'rgb(0,156,222)',
                'remove-color': '#c92424',
                'background-color': 'rgb(237, 237, 237)'
            },
            colors: {
                'color-main': 'rgb(0,156,222)',
                'remove-color': '#c92424',
                'default-line': 'rgb(11, 112, 155)'
            },
            fontFamily: {
                default: ['Mynerve'],
            },
        },
    },
    plugins: [],
}