<script setup lang="ts">
import type { BoardQuery } from '~/domain'

const searchForm = ref<Partial<BoardQuery.Props>>({
  limit: 24,
  offset: 0
})

const query = computed((): Partial<BoardQuery.Props> => {
  return {
    ...searchForm.value
  }
})

const { data: resources, error, status } = await useApi().getBoards(query)

async function handleDeleteClick(boardId: number): Promise<void> {
  const answer = window.confirm('Are you sure you want to delete the board?')

  if (answer) {
    await useApi().deleteBoard(boardId)
    //const { data: resources } = await useApi().getBoards(query)
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
