<script setup lang="ts">
import type { BoardForm } from '~/forms'

const route = useRoute()
const store = useBoardStore()
const { resource } = storeToRefs(store)

async function handleSubmit(form: BoardForm): Promise<void> {
  store.update(form.id, form)
}

onBeforeMount(async () => {
  if (route.params.id)
    useLoaderStore().loadAndAwait(BOARD_COMPONENT_LOADER_KEY, async () => {
      await store.get(parseInt(route.params.id as string))
    })
})
</script>

<template>
  <VContainer>
    <BaseLoader :component-name="BOARD_COMPONENT_LOADER_KEY">
      <template #loader>
        <VSkeletonLoader type="card" />
      </template>
      <GameBoardEditor :board="resource" :reset-on-submit="false" @on-submit="handleSubmit" />
    </BaseLoader>
  </VContainer>
</template>
