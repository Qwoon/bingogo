<script setup lang="ts">
const boardStore = useBoardStore();
const { resource } = storeToRefs(boardStore);

onBeforeMount(async () => {
  await boardStore.get(1, {});
});
</script>
<template>
  <section
    v-if="resource"
    class="h-screen d-flex flex-wrap justify-center align-center"
  >
    <GameBoard :tiles="resource.tiles" />
  </section>
</template>
