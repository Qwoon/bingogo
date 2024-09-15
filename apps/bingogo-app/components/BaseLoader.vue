<script setup lang="ts">
defineProps<{
  componentName: string
}>()

const store = useLoaderStore()
</script>

<template>
  <ClientOnly>
    <slot name="loader" v-if="store.isLoading(componentName)">
      <div class="d-flex justify-center align-center h-100">
        <VProgressCircular size="100" indeterminate color="primary" />
      </div>
    </slot>

    <slot v-else />
  </ClientOnly>
</template>
