<script setup lang="ts">
import type { BoardForm } from '~/forms'

const route = useRoute()

const notificationStore = useNotificationStore()

const boardStore = useBoardStore()
const { resource } = storeToRefs(boardStore)

const boardTileStore = useBoardTileStore()

const boardLocal = ref()

async function handleSubmit(form: BoardForm): Promise<void> {
  // Update board
  await boardStore.update(form.id, form)

  // Update Board -> Tiles
  const nonPersistantTiles = form.tiles.filter((x) => !x.id)
  if (nonPersistantTiles.length > 0) await boardTileStore.create(nonPersistantTiles)

  // Create new Board -> Tiles
  const props = Object.fromEntries(form.tiles.filter((t) => t.id).map((t) => [t.id, { ...t }]))
  await boardTileStore.update(props)

  // Delete Board -> Tiles
  const propsToDelete = resource.value.tiles.filter(
    (t) =>
      form.tiles
        .filter((x) => x.id)
        .map((x) => x.id)
        .indexOf(t.id) === -1
  )

  for (const prop of propsToDelete) await boardTileStore.remove(prop.id)

  notificationStore.setMessage('Changes saved.')
}

onBeforeMount(async () => {
  if (route.params.id)
    useLoaderStore().loadAndAwait(BOARD_COMPONENT_LOADER_KEY, async () => {
      await boardStore.get(parseInt(route.params.id as string))
      boardLocal.value = resource.value
    })
})
</script>

<template>
  <VContainer>
    <BaseLoader :component-name="BOARD_COMPONENT_LOADER_KEY">
      <template #loader>
        <VSkeletonLoader type="card" />
      </template>
      <GameBoardEditor
        cardTitle="Board editor"
        :board="boardLocal"
        :reset-on-submit="false"
        :mode="'update'"
        @on-submit="handleSubmit"
      />
    </BaseLoader>
  </VContainer>
</template>
