<script setup lang="ts">
import type { BoardForm } from '~/forms'

const store = useBoardStore()

async function handleSubmit(form: BoardForm): Promise<void> {
  store.create([form])
}
</script>

<template>
  <VContainer>
    <GameBoardEditor @on-submit="handleSubmit" />
  </VContainer>
</template>
