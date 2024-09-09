<script setup lang="ts">
const boardStore = useBoardStore()
const { resource } = storeToRefs(boardStore)

const route = useRoute()
onBeforeMount(async () => {
  if (route.params.id) {
    await useLoaderStore().loadAndAwait(BOARD_COMPONENT_LOADER_KEY, async () => {
      await boardStore.get(parseInt(route.params.id as string))
    })
  } else navigateTo('/')
})
</script>
<template>
  <section v-if="resource" class="h-screen d-flex flex-wrap justify-center align-center">
    <BaseLoader :component-name="BOARD_COMPONENT_LOADER_KEY">
      <template #loader> Loading... </template>
      <GameBoard :tiles="resource.tiles" />
    </BaseLoader>
  </section>
</template>
