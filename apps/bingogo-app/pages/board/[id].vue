<script setup lang="ts">
import { BoardTile } from '~/domain'

const { params } = useRoute()
const boardId = ref<string>(params.id as string)
const isEditMode = ref<boolean>(false)

//const { data: resource } = await useApi().getBoardById(parseInt(boardId.value))
const resource = ref<BoardTile.Props[]>([
  { title: 'Hello world!', isChecked: false, points: 10, boardId: 1 },
  { title: 'Hello world 2!', isChecked: false, points: 10, boardId: 1 }
])
</script>
<template>
  <section
    v-if="resource"
    class="h-screen d-flex flex-wrap justify-center align-center position-relative"
  >
    <VBtn
      :icon="isEditMode ? 'mdi-check' : 'mdi-pencil'"
      variant="text"
      class="position-absolute"
      style="top: 16px; right: 16px"
      @click="isEditMode = !isEditMode"
    />
    <GameBoard :tiles="resource" :is-edit-mode="isEditMode" />
  </section>
</template>
