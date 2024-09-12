<script setup lang="ts">
interface Props {
  rowIndex: number;
  colIndex: number;
  title: string;
  isChecked: boolean;
}

interface Emit {
  (e: 'update:isChecked', rowIndex: number, colIndex: number): void;
}

const props = defineProps<Props>();
const emit = defineEmits<Emit>();

const { $anime } = useNuxtApp();

// const isChecked = computed(() => (rowIndex: number, colIndex: number) => {
//   return tiles.value[rowIndex][colIndex].isChecked;
// });

function onTileCheck(
  elementId: string,
  rowIndex: number,
  colIndex: number
): void {
  flipCard(elementId);

  emit('update:isChecked', rowIndex, colIndex);
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
</script>

<template>
  <VHover>
    <template v-slot:default="{ isHovering, props }">
      <slot>
        <div class="position-relative" style="backface-visibility: hidden">
          <VCard
            v-bind="props"
            :elevation="isHovering ? 16 : 6"
            width="150"
            height="150"
            class="card-back position-absolute d-flex justify-center align-center cursor-pointer position-relative"
            style="backface-visibility: hidden"
            :id="`b-${rowIndex}-${colIndex}`"
            @click="onTileCheck(`${rowIndex}-${colIndex}`, rowIndex, colIndex)"
            :class="isChecked ? 'bg-primary' : 'bg-secondary'"
          >
            <Icon
              name="ic:round-check-circle"
              size="64"
              class="position-absolute mx-auto right-0 left-0 opacity-50"
            />
            <VCardTitle class="tile-text d-flex flew-wrap text-wrap">
              {{ title }}
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
            @click="onTileCheck(`${rowIndex}-${colIndex}`, rowIndex, colIndex)"
            :class="isChecked ? 'bg-primary' : 'bg-secondary'"
          >
            <VCardTitle class="tile-text d-flex flew-wrap text-wrap">
              {{ title }}
            </VCardTitle>
          </VCard>
        </div>
      </slot>
    </template>
  </VHover>
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
