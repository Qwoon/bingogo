<script setup lang="ts">
interface Board {
  title: string;
  tiles: Tile[][];
}

interface Tile {
  title: string;
  isChecked: boolean;
}

let tiles = ref<Tile[][]>([
  [
    {
      title: 'Нереалистичноооооооo',
      isChecked: false,
    },
    {
      title: 'Казик через 10 мин',
      isChecked: false,
    },
  ],
  [
    {
      title: 'Это баг?',
      isChecked: false,
    },
    {
      title: 'Даун на разрабе',
      isChecked: false,
    },
  ],
  [
    {
      title: 'Кто я? Где я?',
      isChecked: false,
    },
    {
      title: 'Опять гайды',
      isChecked: false,
    },
  ],
]);

let uncheckedTilesCount = ref<number>();

const isChecked = computed(() => (rowIndex: number, colIndex: number) => {
  return tiles.value[rowIndex][colIndex].isChecked;
});

onBeforeMount(() => {
  uncheckedTilesCount.value = tiles.value
    .flatMap((x) => x)
    .map((x) => x.isChecked)
    .filter((x) => !x).length;
});

watch(uncheckedTilesCount, (newValue) => {
  if (newValue === 0) setTimeout(() => endGame(), 300);
});

function onTileCheck(rowIndex: number, colIndex: number): void {
  tiles.value[rowIndex][colIndex].isChecked =
    !tiles.value[rowIndex][colIndex].isChecked;

  uncheckedTilesCount.value = tiles.value
    .flatMap((x) => x)
    .map((x) => x.isChecked)
    .filter((x) => !x).length;
}

function endGame(): void {
  alert('Play Animation');

  // reset
  for (let i in tiles.value)
    for (let k in tiles.value[i]) tiles.value[i][k].isChecked = false;
}
</script>

<template>
  <section class="h-screen d-flex flex-wrap justify-center align-center">
    <div class="grid">
      <VRow v-for="(row, rowIndex) in tiles">
        <VCol v-for="(col, colIndex) in row">
          <VHover>
            <template #default="{ isHovering, props }">
              <VCard
                v-bind="props"
                :elevation="isHovering ? 15 : 6"
                width="150"
                height="150"
                class="d-flex justify-center align-center cursor-pointer"
                @click="onTileCheck(rowIndex, colIndex)"
                :class="
                  isChecked(rowIndex, colIndex) ? 'bg-primary' : 'bg-secondary'
                "
              >
                <VCardTitle class="tile-text d-flex flex-wrap text-wrap">
                  {{ col.title }}
                </VCardTitle>
              </VCard>
            </template>
          </VHover>
        </VCol>
      </VRow>
    </div>
  </section>
</template>

<style scoped langs="scss">
.tile-text {
  width: fit-content;
  word-break: break-word;
  text-align: center;
}
</style>
