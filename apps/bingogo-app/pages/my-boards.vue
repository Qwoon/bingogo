<script setup lang="ts">
interface Game {
  id: number;
  title: string;
  link: string;
  createdBy: string;
  createdAt: Date;
}

const games: Game[] = [
  {
    id: 1,
    title: 'Papich Bingo',
    createdAt: new Date(),
    createdBy: 'Qwoon',
    link: '1',
  },
  {
    id: 1,
    title: 'Papich Bingo 2',
    createdAt: new Date(),
    createdBy: 'Qwoon',
    link: '1',
  },
];

onBeforeMount(async () => {
  await useBoardStore().getList();
  // console.log(useRuntimeConfig().public);
});

async function onGameClick(gameId: number): Promise<void> {
  await navigateTo({ path: `board/${gameId}` });
}
</script>

<template>
  <VContainer class="h-screen">
    <VRow>
      <VCol cols="12" md="12" v-for="game in games">
        <VHover>
          <template #default="{ isHovering, props }">
            <VCard
              :title="game.title"
              class="pa-5 cursor-pointer"
              v-bind="props"
              :elevation="isHovering ? 16 : 6"
              @click="onGameClick(game.id)"
            >
              <VCardSubtitle
                >{{ game.createdAt }}, by {{ game.createdBy }}</VCardSubtitle
              >
            </VCard>
          </template>
        </VHover>
      </VCol>
    </VRow>
  </VContainer>
</template>
