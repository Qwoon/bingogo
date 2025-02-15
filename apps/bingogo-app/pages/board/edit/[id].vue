<script setup lang="ts">
import type { Board } from '~/domain/board'
import type { BoardForm } from '~/forms'

const route = useRoute()

const notificationStore = useNotificationStore()
const boardStore = useBoardStore()
const boardTileStore = useBoardTileStore()

const boardId = ref<string>(route.params.id as string)
const {
  data: boardLocal,
  refresh,
  status
} = await useLazyFetch<Board>(() => `${useRuntimeConfig().public.apiBase}/boards/${boardId.value}`)

onBeforeMount(async () => {
  if (boardId) {
    refresh()
  } else navigateTo('/')
})

async function handleSubmit(form: BoardForm): Promise<void> {
  // Update board
  await boardStore.update(form.id, form)

  // Create new Board -> Tiles
  const nonPersistantTiles = form.tiles.filter((x) => !x.id)
  if (nonPersistantTiles.length > 0) {
    await boardTileStore.create(nonPersistantTiles)
  }
  // Update Board -> Tiles
  const props = Object.fromEntries(form.tiles.filter((t) => t.id).map((t) => [t.id, { ...t }]))
  await boardTileStore.update(props)

  // Delete Board -> Tiles
  const propsToDelete = boardLocal.value.tiles.filter(
    (t) =>
      form.tiles
        .filter((x) => x.id)
        .map((x) => x.id)
        .indexOf(t.id) === -1
  )

  for (const prop of propsToDelete) await boardTileStore.remove(prop.id)

  await refresh()

  notificationStore.setMessage('Changes saved.')
}
</script>

<template>
  <BaseServerLoader :is-loading="!boardLocal || status === 'pending'">
    <template #default>
      <VContainer class="h-100">
        <GameBoardEditor
          cardTitle="Board editor"
          :board="boardLocal"
          :reset-on-submit="false"
          :mode="'update'"
          @on-submit="handleSubmit"
        />
      </VContainer>
    </template>
  </BaseServerLoader>
</template>
