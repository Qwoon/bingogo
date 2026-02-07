<script setup lang="ts">
import { BoardTile } from '~/domain'

//const { data: resource } = await useApi().getBoardById(parseInt(boardId.value))
const resource = ref<BoardTile.Props[]>([
  { title: 'Hello world!', isChecked: false, points: 10, boardId: 1 },
  { title: 'Hello world 2!', isChecked: false, points: 10, boardId: 1 }
])
</script>
<template>
  <section v-if="resource" class="h-screen d-flex flex-wrap justify-center align-center">
    <GameBoard :tiles="resource" />
  </section>
</template>
