<script setup lang="ts">
const store = useBoardStore()
const { resources } = storeToRefs(store)

onBeforeMount(() => {
  useLoaderStore().loadAndAwait(BOARDS_COMPONENT_LOADER_KEY, async () => {
    await store.getList()
  })
})

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
      <BaseLoader :component-name="BOARDS_COMPONENT_LOADER_KEY">
        <template #loader> Loading... </template>
        <VCol cols="12" sm="12" md="6" v-for="board in resources" :key="board.id">
          <GameBoardCard :board="board" @on-delete-click="handleDeleteClick"></GameBoardCard>
        </VCol>
      </BaseLoader>
    </VRow>
  </VContainer>
</template>
