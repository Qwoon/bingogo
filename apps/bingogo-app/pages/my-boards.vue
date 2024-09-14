<script setup lang="ts">
const store = useBoardStore()
const { resources } = storeToRefs(store)

onBeforeMount(() => {
  useLoaderStore().loadAndAwait(BOARDS_COMPONENT_LOADER_KEY, async () => {
    await store.getList()
  })
})
</script>

<template>
  <VContainer class="h-screen">
    <VRow>
      <BaseLoader :component-name="BOARDS_COMPONENT_LOADER_KEY">
        <template #loader> Loading... </template>
        <VCol cols="12" sm="12" md="6" v-for="board in resources" :key="board.id">
          <GameBoardCard :board="board"></GameBoardCard>
        </VCol>
      </BaseLoader>
    </VRow>
  </VContainer>
</template>
