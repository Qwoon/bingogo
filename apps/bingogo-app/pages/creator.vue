<script setup lang="ts">
import type { BoardForm } from '~/forms'

async function handleSubmit(form: BoardForm): Promise<void> {
  await useApi().createBoard([form])
  useNotificationStore().setMessage('Board created')
}
</script>

<template>
  <VContainer>
    <GameBoardEditor
      @on-submit="handleSubmit"
      :mode="'create'"
      :cardTitle="'Board creator'"
      :cardSubtitle="'Create your own & unique bingo board'"
    />
  </VContainer>
</template>
