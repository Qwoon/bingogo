<script setup lang="ts">
import type { Board } from '~/domain'

const route = useRoute()
const boardId = ref<string>(route.params.id as string)

const {
  data: resource,
  refresh,
  status
} = await useLazyFetch<Board>(
  () => `${useRuntimeConfig().public.apiBase}/boards/${boardId.value}`,
  {}
)

onBeforeMount(async () => {
  if (boardId) {
    refresh()
  } else navigateTo('/')
})
</script>
<template>
  <section v-if="resource" class="h-screen d-flex flex-wrap justify-center align-center">
    <GameBoard :tiles="resource.tiles" />
  </section>
</template>
