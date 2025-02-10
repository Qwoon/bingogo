<script setup lang="ts">
import type { HomeCardHowItWorks } from '#build/components'
</script>

<template>
  <VCard class="pa-5 ma-2 w-75 h-75" color="">
    <VCardTitle>Bingogo v0.0.2 <Icon name="mdi:gamepad-variant-outline"></Icon></VCardTitle>
    <VCardSubtitle>Create your own bingo game and play with friends</VCardSubtitle>
    <VCardItem>
      <VRow>
        <VCol cols="12" sm="12" md="6">
          <HomeCardLogin />
        </VCol>
        <VCol cols="12" sm="12" md="6">
          <HomeCardHowItWorks />
        </VCol>
      </VRow>
    </VCardItem>
  </VCard>
</template>
