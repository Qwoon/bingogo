<script setup lang="ts">
import type { Board } from '~/domain'

interface Props {
  board: Board.Props
}

defineProps<Props>()

const menu = ref<boolean>(false)

const subtitle = computed(() => (board: Board.Props) => {
  return `${board.createdAt}, by ${board.createdById ?? 'Anonymous'}
`
})

async function onGameClick(gameId: number): Promise<void> {
  await navigateTo({ path: `board/${gameId}` })
}

async function onEditClick(gameId: number): Promise<void> {
  await navigateTo({ path: `board/edit/${gameId}` })
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
        @click="onGameClick(board.id)"
      >
        <template #append>
          <div>
            <VMenu v-model="menu">
              <template v-slot:activator="{ props: activatorProps }">
                <VBtn icon color="default" variant="text" v-bind="activatorProps">
                  <VIcon size="24" icon="mdi-dots-vertical" />
                </VBtn>
              </template>
              <template #default="{ isActive }">
                <VList>
                  <VListItem link @click="onEditClick(board.id)">
                    <VListItemTitle> Edit </VListItemTitle>
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
