<script setup lang="ts">
const store = useGameStore();

const { currentGameName } = storeToRefs(store);
</script>

<template>
  <VAppBar app dense scroll-behavior="hide" class="bg-transparent">
    <template #prepend>
      <VAppBarNavIcon></VAppBarNavIcon>
    </template>

    <template #title v-if="$vuetify.display.mdAndUp">
      {{ currentGameName }}
    </template>

    <template #default>
      <div class="d-flex align-center ga-2">
        <span>Connected</span>
        <span class="status-indicator bg-green rounded-circle"></span>
      </div>

      <VSpacer />
    </template>

    <template #append>
      <VBtn append-icon="mdi-login" variant="elevated"> Login </VBtn>
    </template>
  </VAppBar>
</template>

<style scoped lang="scss">
.status-indicator {
  height: 10px;
  width: 10px;
}
</style>
