<script setup lang="ts">
import type { BoardForm } from '~/forms'

const store = useBoardStore()

async function handleSubmit(form: BoardForm): Promise<void> {
  store.create([form])
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
