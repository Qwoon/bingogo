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
  alert('Play Animation');

  // reset
  for (let i in tiles.value)
    for (let k in tiles.value[i]) tiles.value[i][k].isChecked = false;

  // Flip cards back
}
</script>

<template>
  <section class="h-screen d-flex flex-wrap justify-center align-center">
    <div class="grid">
      <VRow v-for="(row, rowIndex) in tiles">
        <VCol v-for="(col, colIndex) in row">
          <VHover>
            <template v-slot:default="{ isHovering, props }">
              <div
                class="position-relative"
                style="backface-visibility: hidden"
              >
                <VCard
                  v-bind="props"
                  :elevation="isHovering ? 15 : 6"
                  width="150"
                  height="150"
                  class="card-back position-absolute d-flex justify-center align-center cursor-pointer relative"
                  style="backface-visibility: hidden"
                  :id="`b-${rowIndex}-${colIndex}`"
                  @click="
                    onTileCheck(`${rowIndex}-${colIndex}`, rowIndex, colIndex)
                  "
                  :class="
                    isChecked(rowIndex, colIndex)
                      ? 'bg-primary'
                      : 'bg-secondary'
                  "
                >
                  <VCardTitle class="tile-text d-flex flew-wrap text-wrap">
                    Flipped
                  </VCardTitle>
                </VCard>
                <VCard
                  v-bind="props"
                  :elevation="isHovering ? 15 : 6"
                  width="150"
                  height="150"
                  class="d-flex position-relative justify-center align-center cursor-pointer relative"
                  style="backface-visibility: hidden"
                  :id="`f-${rowIndex}-${colIndex}`"
                  @click="
                    onTileCheck(`${rowIndex}-${colIndex}`, rowIndex, colIndex)
                  "
                  :class="
                    isChecked(rowIndex, colIndex)
                      ? 'bg-primary'
                      : 'bg-secondary'
                  "
                >
                  <VCardTitle class="tile-text d-flex flew-wrap text-wrap">
                    {{ col.title }}
                  </VCardTitle>
                </VCard>
              </div>
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

.card-back {
  top: 0;
  left: 0px;
  position: absolute;
}
</style>
