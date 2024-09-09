<script setup lang="ts">
const boardStore = useBoardStore()
const { resource } = storeToRefs(boardStore)

const route = useRoute()
onBeforeMount(async () => {
  if (route.params.id) await boardStore.get(parseInt(route.params.id as string))
  else navigateTo('/')
})
</script>
<template>
  <section v-if="resource" class="h-screen d-flex flex-wrap justify-center align-center">
    <GameBoard :tiles="resource.tiles" />
  </section>
</template>
