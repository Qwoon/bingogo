<script setup lang="ts">
import { BoardTile } from '~/domain';

const props = defineProps<{
  tiles: BoardTile.Props[];
}>();

let tiles = ref<BoardTile.Props[][]>();

let uncheckedTilesCount = ref<number>();

const isChecked = computed(() => (rowIndex: number, colIndex: number) => {
  return tiles.value[rowIndex][colIndex].isChecked;
});

const { $anime } = useNuxtApp();

onBeforeMount(() => {
  let initTiles = props.tiles;

  if (initTiles.length % 6 !== 0)
    while (initTiles.length % 6 !== 0)
      initTiles.push(BoardTile.constructEmpty());

  tiles.value = useListToMatrix(initTiles, 6);

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
      if (!tiles.value[i][k].isMock) {
        tiles.value[i][k].isChecked = false;
        flipCard(`${i}-${k}`);
      }
    }

  // Flip cards back
}
</script>

<template>
  <VContainer>
    <VRow v-for="(row, rowIndex) in tiles" class="justify-center">
      <VCol v-for="(col, colIndex) in row" :style="{ 'flex-grow': 0 }">
        <GameBoardTile
          v-if="!col.isMock"
          :title="col.title"
          :row-index="rowIndex"
          :col-index="colIndex"
          :is-checked="isChecked(rowIndex, colIndex)"
          @update:is-checked="
            onTileCheck(`${rowIndex}-${colIndex}`, rowIndex, colIndex)
          "
        />
        <VCard
          v-else
          :elevation="6"
          width="150"
          height="150"
          class="card-back position-relative d-flex justify-center align-center cursor-pointer position-relative"
          :class="isChecked ? 'bg-primary' : 'bg-secondary'"
        >
          <Icon
            name="ic:round-check-circle"
            size="64"
            class="position-absolute mx-auto right-0 left-0 opacity-50"
          />
        </VCard>
      </VCol>
    </VRow>
  </VContainer>
</template>
