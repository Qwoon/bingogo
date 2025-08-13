<script setup lang="ts">
const { params } = useRoute()
const boardId = ref<string>(params.id as string)

const { data: resource } = await useApi().getBoardById(parseInt(boardId.value))
</script>
<template>
  <section v-if="resource" class="h-screen d-flex flex-wrap justify-center align-center">
    <GameBoard :tiles="resource.tiles" />
  </section>
</template>
