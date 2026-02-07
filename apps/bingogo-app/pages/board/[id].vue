<script setup lang="ts">
import { BoardTile } from '~/domain'

const { params } = useRoute()
const boardId = ref<string>(params.id as string)

//const { data: resource } = await useApi().getBoardById(parseInt(boardId.value))
const resource = ref<BoardTile.Props[]>([{ title: '', isChecked: false, points: 10, boardId: 1 }])
</script>
<template>
  <section v-if="resource" class="h-screen d-flex flex-wrap justify-center align-center">
    <GameBoard :tiles="resource" />
  </section>
</template>
