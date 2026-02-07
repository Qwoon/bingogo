<script setup lang="ts">
import { BoardTile } from '~/domain'
import Tile from './Tile.vue'

const props = defineProps<{
  tiles: BoardTile.Props[]
}>()

const tiles = ref<BoardTile.Props[][]>()
const childrensRefs = ref<InstanceType<typeof Tile>[]>()

const isChecked = computed(() => (rowIndex: number, colIndex: number) => {
  return tiles.value[rowIndex][colIndex].isChecked
})

const uncheckedTilesCount = computed<number>(
  () =>
    (uncheckedTilesCount.value = tiles.value
      ?.flatMap((x) => x)
      ?.map((x) => x.isChecked)
      ?.filter((x) => !x).length)
)

onMounted(() => {
  let initTiles = props.tiles

  if (initTiles.length % 6 !== 0)
    while (initTiles.length % 6 !== 0) initTiles.push(BoardTile.constructEmpty())

  tiles.value = listToMatrix(initTiles, 6)
})

watch(uncheckedTilesCount, (newValue: number) => {
  if (newValue === 0) setTimeout(() => endGame(), 200)
})

function onTileCheck(rowIndex: number, colIndex: number): void {
  tiles.value[rowIndex][colIndex].isChecked = !tiles.value[rowIndex][colIndex].isChecked
}

function endGame(): void {
  useStartParty()

  // reset
  for (let i in tiles.value)
    for (let k in tiles.value[i]) {
      if (!tiles.value[i][k].isMock) {
        tiles.value[i][k].isChecked = false
      }
    }

  for (const c of childrensRefs.value) c.flipTile()
}
</script>

<template>
  <VContainer>
    <VRow v-for="(row, rowIndex) in tiles" class="justify-center">
      <VCol v-for="(col, colIndex) in row" :style="{ 'flex-grow': 0 }">
        <GameBoardTile
          v-if="!col.isMock"
          ref="childrensRefs"
          :title="col.title"
          :row-index="rowIndex"
          :col-index="colIndex"
          :is-checked="isChecked(rowIndex, colIndex)"
          @update:is-checked="onTileCheck(rowIndex, colIndex)"
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
