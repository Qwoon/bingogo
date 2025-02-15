<script setup lang="ts">
import type { Board } from '~/domain'

const store = useBoardStore()

const {
  data: resources,
  refresh,
  status
} = await useLazyFetch<Board[]>(() => `${useRuntimeConfig().public.apiBase}/boards`, {})

onBeforeMount(async () => refresh())

async function handleDeleteClick(boardId: number): Promise<void> {
  const answer = window.confirm('Are you sure you want to delete the board?')

  if (answer) {
    await store.remove(boardId)
    const index = resources.value.findIndex((x) => x.id === boardId)
    resources.value.splice(index, 1)
  }
}
</script>

<template>
  <VContainer class="h-screen">
    <VRow>
      <BaseServerLoader :is-loading="!resources || status === 'pending'">
        <template #loader>
          <VCol cols="12">
            <VProgressCircular size="100" indeterminate color="primary" />
          </VCol>
        </template>
        <template #default>
          <VCol cols="12" sm="12" md="6" v-for="board in resources" :key="board.id">
            <GameBoardCard :board="board" @on-delete-click="handleDeleteClick"></GameBoardCard>
          </VCol>
        </template>
      </BaseServerLoader>
    </VRow>
  </VContainer>
</template>
