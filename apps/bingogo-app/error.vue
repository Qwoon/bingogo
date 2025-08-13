<script setup lang="ts">
import type { NuxtError } from '#app'

const props = defineProps({
  error: Object as () => NuxtError
})

const handleError = () => clearError({ redirect: '/' })
</script>

<template>
  <VContainer max-width="1640px" style="margin-top: 100px">
    <VCard class="text-center">
      <VCardItem class="justify-center">
        <VCardText class="text-grey text-uppercase font-weight-bold">
          {{ error?.statusCode }} - {{ error?.message }}
        </VCardText>
      </VCardItem>
      <VCardActions class="justify-center">
        <NuxtLink to="/" class="text-decoration-none text-uppercase font-weight-bold"
          >Return Home</NuxtLink
        >
      </VCardActions>
    </VCard>
  </VContainer>
</template>
