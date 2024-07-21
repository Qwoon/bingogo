<script setup lang="ts">
const store = useBoardStore();
const { resources } = storeToRefs(store);

onBeforeMount(async () => {
  await store.getList();
});

async function onGameClick(gameId: number): Promise<void> {
  await navigateTo({ path: `board/${gameId}` });
}
</script>

<template>
  <VContainer class="h-screen">
    <VRow>
      <VCol cols="12" md="12" v-for="board in resources">
        <VHover>
          <template #default="{ isHovering, props }">
            <VCard
              :title="board.title"
              class="pa-5 cursor-pointer"
              v-bind="props"
              :elevation="isHovering ? 16 : 6"
              @click="onGameClick(board.id)"
            >
              <VCardSubtitle
                >{{ board.createdAt }}, by
                {{ board.createdById }}</VCardSubtitle
              >
            </VCard>
          </template>
        </VHover>
      </VCol>
    </VRow>
  </VContainer>
</template>
