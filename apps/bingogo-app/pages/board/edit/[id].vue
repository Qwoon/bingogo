<script setup lang="ts">
import type { Board } from '~/domain/board'
import type { BoardForm } from '~/forms'

const { params } = useRoute()

const notificationStore = useNotificationStore()

const boardId = ref<string>(params.id as string)
const { data: boardLocal, status } = await useLazyFetch<Board>(
  () => `${useRuntimeConfig().public.apiBase}/boards/${boardId.value}`
)

async function handleSubmit(form: BoardForm): Promise<void> {
  // Update board
  await useApi().updateBoard(form.id, form)

  // Create new Board -> Tiles
  const nonPersistantTiles = form.tiles.filter((x) => !x.id)
  if (nonPersistantTiles.length > 0) {
    await useApi().createBoardTile(nonPersistantTiles)
  }
  // Update Board -> Tiles
  const props = Object.fromEntries(form.tiles.filter((t) => t.id).map((t) => [t.id, { ...t }]))
  await useApi().updateBoardTiles(props)

  // Delete Board -> Tiles
  const propsToDelete = boardLocal.value.tiles.filter(
    (t) =>
      form.tiles
        .filter((x) => x.id)
        .map((x) => x.id)
        .indexOf(t.id) === -1
  )

  for (const prop of propsToDelete) await useApi().deleteBoardTile(prop.id)

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
