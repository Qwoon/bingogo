<script setup lang="ts">
interface Slide {
  title: string
  description: string
  color: string
}

const slides = ref<Slide[]>([
  {
    title: 'Login',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
    color: 'info'
  },
  {
    title: 'Create board',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
    color: 'secondary'
  },
  {
    title: 'Start Closing Tiles',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
    color: 'success'
  }
])
</script>
<template>
  <VCard class="pa-5 ma-2" variant="text">
    <VCardTitle>How it works</VCardTitle>
    <VCarousel show-arrows="hover" cycle hide-delimiter-background>
      <VCarouselItem v-for="slide in slides">
        <VSheet :color="slide.color" height="100%" class="rounded-lg">
          <div class="d-flex flex-column fill-height justify-center align-center pa-8">
            <div class="text-h2">{{ slide.title }}</div>
            <div class="text-subtitle-2">{{ slide.description }}</div>
          </div>
        </VSheet>
      </VCarouselItem>
    </VCarousel>
  </VCard>
</template>
