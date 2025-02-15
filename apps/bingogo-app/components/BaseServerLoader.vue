<script setup lang="ts">
interface Props {
  isLoading: boolean
}

defineProps<Props>()
</script>

<template>
  <slot name="loader" v-if="isLoading">
    <div class="d-flex justify-center align-center h-100">
      <VProgressCircular size="100" indeterminate color="primary" />
    </div>
  </slot>

  <slot v-else />
</template>
