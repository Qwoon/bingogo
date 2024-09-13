<script setup lang="ts">
defineProps<{
  componentName: string
}>()

const store = useLoaderStore()
</script>

<template>
  <ClientOnly>
    <slot name="loader" v-if="store.isLoading(componentName)">
      <VProgressCircular indeterminate color="primary" />
    </slot>

    <slot v-else />
  </ClientOnly>
</template>
