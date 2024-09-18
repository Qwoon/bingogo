<script setup lang="ts">
import type { Board } from '~/domain'

interface Emits {
  (e: 'onDeleteClick', boardId: number): void
}

interface Props {
  board: Board.Props
}

const emit = defineEmits<Emits>()
defineProps<Props>()

const menu = ref<boolean>(false)

const subtitle = computed(() => (board: Board.Props) => {
  return `${board.createdAt}, by ${board.createdById ?? 'Anonymous'}`
})

async function onBoardClick(boardId: number): Promise<void> {
  await navigateTo({ path: `board/${boardId}` })
}

async function onEditClick(boardId: number): Promise<void> {
  await navigateTo({ path: `board/edit/${boardId}` })
}

async function onDeleteClick(boardId: number): Promise<void> {
  emit('onDeleteClick', boardId)
}
</script>

<template>
  <VHover>
    <template #default="{ isHovering, props }">
      <VCard
        :title="board.title"
        :subtitle="subtitle(board)"
        class="pa-5 cursor-pointer"
        v-bind="props"
        :elevation="isHovering ? 16 : 6"
        @click="onBoardClick(board.id)"
      >
        <template #append>
          <div>
            <VMenu min-width="100px" v-model="menu">
              <template v-slot:activator="{ props: activatorProps }">
                <VBtn icon color="default" variant="text" v-bind="activatorProps">
                  <VIcon size="24" icon="mdi-dots-vertical" />
                </VBtn>
              </template>
              <template #default="{ isActive }">
                <VList>
                  <VListItem link @click="onEditClick(board.id)">
                    <VListItemTitle> Edit Board </VListItemTitle>
                  </VListItem>
                  <VListItem link @click="onDeleteClick(board.id)">
                    <VListItemTitle> Delete Board </VListItemTitle>
                  </VListItem>
                </VList>
              </template>
            </VMenu>
          </div>
        </template>
      </VCard>
    </template>
  </VHover>
</template>
