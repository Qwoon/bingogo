import { fileURLToPath } from 'url';
// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },

  alias: {
    images: fileURLToPath(new URL('./assets/images', import.meta.url)),
    style: fileURLToPath(new URL('./assets/style', import.meta.url)),
  },

  routeRules: {
    '/': { prerender: true },
  },

  vuetify: {
    moduleOptions: {},
    vuetifyOptions: './vuetify.config.ts',
  },

  modules: [
    '@nuxt/fonts',
    'vuetify-nuxt-module',
    '@vueuse/nuxt',
    'nuxt-svgo',
    '@hypernym/nuxt-anime',
    'nuxt-icon',
    '@pinia/nuxt',
  ],
});
