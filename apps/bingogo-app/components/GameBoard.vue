<script setup lang="ts">
import type { BoardTile } from '~/api';

let tiles = ref<BoardTile.Props[][]>([
  [
    {
      title: 'Нереалистичноооооооo',
      isChecked: false,
      points: 0,
      boardId: 1,
    },
    {
      title: 'Казик через 10 мин',
      isChecked: false,
      points: 0,
      boardId: 1,
    },
  ],
  [
    {
      title: 'Это баг?',
      isChecked: false,
      points: 0,
      boardId: 1,
    },
    {
      title: 'Даун на разрабе',
      isChecked: false,
      points: 0,
      boardId: 1,
    },
  ],
  [
    {
      title: 'Кто я? Где я?',
      isChecked: false,
      points: 0,
      boardId: 1,
    },
    {
      title: 'Опять гайды',
      isChecked: false,
      points: 0,
      boardId: 1,
    },
  ],
]);

let uncheckedTilesCount = ref<number>();

const isChecked = computed(() => (rowIndex: number, colIndex: number) => {
  return tiles.value[rowIndex][colIndex].isChecked;
});

const { $anime } = useNuxtApp();

onBeforeMount(() => {
  uncheckedTilesCount.value = tiles.value
    .flatMap((x) => x)
    .map((x) => x.isChecked)
    .filter((x) => !x).length;
});

watch(uncheckedTilesCount, (newValue) => {
  if (newValue === 0) setTimeout(() => endGame(), 400);
});

function onTileCheck(
  elementId: string,
  rowIndex: number,
  colIndex: number
): void {
  flipCard(elementId);

  tiles.value[rowIndex][colIndex].isChecked =
    !tiles.value[rowIndex][colIndex].isChecked;

  uncheckedTilesCount.value = tiles.value
    .flatMap((x) => x)
    .map((x) => x.isChecked)
    .filter((x) => !x).length;
}

// FIXME: It might be a better idea to flip +180deg and second time -180deg
// so that we dont go above 360deg
function flipCard(elementId: string) {
  $anime({
    targets: [`#f-${elementId}`],
    scale: [{ value: 1 }, { value: 1 }, { value: 1 }],
    rotateY: { value: '+=180', delay: 200 },
    easing: 'easeInOutSine',
    duration: 200,
  });

  // this is a shit-hack, since I couldn't resolve it with CSS.
  // Anime.js resets the transformation and then rotates the element
  // Which causes the element to rotate 360 deg from the point of 180
  $anime({
    targets: [`#b-${elementId}`],
    scale: [{ value: 1 }, { value: 1 }, { value: 1 }],
    rotateY: { value: '+=360', delay: 200 },
    easing: 'easeInOutSine',
    duration: 200,
  });
}

function endGame(): void {
  useStartParty();

  // reset
  for (let i in tiles.value)
    for (let k in tiles.value[i]) {
      tiles.value[i][k].isChecked = false;
      flipCard(`${i}-${k}`);
    }

  // Flip cards back
}
</script>

<template>
  <div class="grid">
    <VRow v-for="(row, rowIndex) in tiles">
      <VCol v-for="(col, colIndex) in row">
        <GameBoardTile
          :title="col.title"
          :row-index="rowIndex"
          :col-index="colIndex"
          :is-checked="isChecked(rowIndex, colIndex)"
          @update:is-checked="
            onTileCheck(`${rowIndex}-${colIndex}`, rowIndex, colIndex)
          "
        />
      </VCol>
    </VRow>
  </div>
</template>
