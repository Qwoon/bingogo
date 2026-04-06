<script setup lang="ts">
import { BoardTile } from '~/domain'
import Tile from './Tile.vue'

const props = defineProps<{
  tiles: BoardTile.Props[]
  isEditMode: boolean
}>()
const winDialog = ref<boolean>(false)
const editDialog = ref<boolean>(false)
const editingTile = ref<{ rowIndex: number; colIndex: number; title: string } | null>(null)
const tiles = ref<BoardTile.Props[][]>()
const childrensRefs = ref<InstanceType<typeof Tile>[]>()
const isDirty = ref<boolean>(false)

let originalTitles: string[] = []

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

const flatRealTiles = computed(() => tiles.value?.flatMap((x) => x).filter((x) => !x.isMock) ?? [])

onMounted(() => {
  buildTileMatrix(props.tiles)
  originalTitles = props.tiles.map((t) => t.title)
})

watch(uncheckedTilesCount, (newValue: number) => {
  if (!props.isEditMode && newValue === 0) setTimeout(() => endGame(), 200)
})

function buildTileMatrix(realTiles: BoardTile.Props[]): void {
  const padded = [...realTiles]
  while (padded.length % 6 !== 0) padded.push(BoardTile.constructEmpty())
  tiles.value = listToMatrix(padded, 6)
}

function onTileCheck(rowIndex: number, colIndex: number): void {
  tiles.value[rowIndex][colIndex].isChecked = !tiles.value[rowIndex][colIndex].isChecked
}

function openEditDialog(rowIndex: number, colIndex: number): void {
  editingTile.value = {
    rowIndex,
    colIndex,
    title: tiles.value[rowIndex][colIndex].title
  }
  editDialog.value = true
}

function confirmTileEdit(): void {
  const { rowIndex, colIndex, title } = editingTile.value!
  tiles.value[rowIndex][colIndex].title = title
  editDialog.value = false
  editingTile.value = null
  checkDirty()
}

function addTile(): void {
  const real = tiles.value!.flatMap((x) => x).filter((t) => !t.isMock)
  real.push({
    title: 'New tile',
    isChecked: false,
    points: 0,
    boardId: props.tiles[0]?.boardId ?? 1
  })
  buildTileMatrix(real)
  checkDirty()
}

function checkDirty(): void {
  const current = flatRealTiles.value.map((t) => t.title)
  isDirty.value =
    current.length !== originalTitles.length || current.some((t, i) => t !== originalTitles[i])
}

function saveChanges(): void {
  originalTitles = flatRealTiles.value.map((t) => t.title)
  isDirty.value = false
  // TODO: persist via API
}

function endGame(): void {
  useStartParty()
  winDialog.value = true
}

function resetGame(): void {
  winDialog.value = false
  for (let i in tiles.value)
    for (let k in tiles.value[i]) if (!tiles.value[i][k].isMock) tiles.value[i][k].isChecked = false
  for (const c of childrensRefs.value!) c.flipTile()
}
</script>

<template>
  <VContainer class="position-relative">
    <!-- Win dialog -->
    <VDialog v-model="winDialog" width="auto">
      <VCard
        max-width="600"
        text="Play again or create new board from scratch!"
        title="Congratulations!"
      >
        <template v-slot:actions>
          <VBtn class="ms-auto" text="Play Again" @click="resetGame" />
        </template>
      </VCard>
    </VDialog>

    <!-- Edit tile dialog -->
    <VDialog v-model="editDialog" max-width="400">
      <VCard title="Edit Tile" v-if="editingTile">
        <VCardText>
          <VTextField
            v-model="editingTile.title"
            label="Tile text"
            autofocus
            @keyup.enter="confirmTileEdit"
          />
        </VCardText>
        <VCardActions>
          <VSpacer />
          <VBtn variant="text" @click="editDialog = false">Cancel</VBtn>
          <VBtn color="primary" variant="flat" @click="confirmTileEdit">Confirm</VBtn>
        </VCardActions>
      </VCard>
    </VDialog>

    <!-- Board grid -->
    <VRow v-for="(row, rowIndex) in tiles" :key="rowIndex" class="justify-center">
      <VCol v-for="(col, colIndex) in row" :key="colIndex" :style="{ 'flex-grow': 0 }">
        <!-- View mode: real tile -->
        <GameBoardTile
          v-if="!col.isMock && !isEditMode"
          ref="childrensRefs"
          :title="col.title"
          :row-index="rowIndex"
          :col-index="colIndex"
          :is-checked="isChecked(rowIndex, colIndex)"
          @update:is-checked="onTileCheck(rowIndex, colIndex)"
        />

        <!-- Edit mode: real tile -->
        <VCard
          v-else-if="!col.isMock && isEditMode"
          :elevation="6"
          width="150"
          height="150"
          class="d-flex justify-center align-center cursor-pointer pa-2 position-relative"
          @click="openEditDialog(rowIndex, colIndex)"
        >
          <Icon
            name="mdi-pencil"
            size="16"
            class="position-absolute opacity-40"
            style="top: 6px; right: 6px"
          />
          <VCardTitle
            class="text-wrap text-center"
            style="font-size: 0.9rem; word-break: break-word"
          >
            {{ col.title }}
          </VCardTitle>
        </VCard>

        <!-- View mode: mock/padding tile -->
        <VCard
          v-else-if="col.isMock && !isEditMode"
          :elevation="6"
          width="150"
          height="150"
          class="position-relative d-flex justify-center align-center"
          :class="isChecked(rowIndex, colIndex) ? 'bg-primary' : 'bg-secondary'"
        >
          <Icon
            name="ic:round-check-circle"
            size="64"
            class="position-absolute mx-auto right-0 left-0 opacity-50"
          />
        </VCard>

        <!-- Edit mode: mock/padding tile — invisible placeholder -->
        <div v-else style="width: 150px; height: 150px" />
      </VCol>
    </VRow>

    <!-- Edit mode action bar -->
    <VRow v-if="isEditMode" class="mt-4 justify-space-between align-center">
      <VCol cols="auto">
        <VBtn variant="outlined" prepend-icon="mdi-plus" @click="addTile">Add Tile</VBtn>
      </VCol>
      <VCol cols="auto">
        <VBtn
          v-if="isDirty"
          color="primary"
          variant="flat"
          prepend-icon="mdi-content-save"
          @click="saveChanges"
        >
          Save
        </VBtn>
      </VCol>
    </VRow>
  </VContainer>
</template>
