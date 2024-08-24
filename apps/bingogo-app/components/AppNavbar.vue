<script setup lang="ts">
const store = useGameStore();
const { currentGameName } = storeToRefs(store);

const navItems = [
  {
    title: 'Home',
    to: { name: 'index' },
  },
  {
    title: 'Create board',
    to: { name: 'creator' },
  },
  {
    title: 'My boards',
    to: { name: 'my-boards' },
  },
];

let showDrawer = ref<boolean>(false);
</script>

<template>
  <VAppBar app dense scroll-behavior="hide" class="bg-transparent">
    <template #prepend>
      <VAppBarNavIcon @click="showDrawer = !showDrawer"></VAppBarNavIcon>
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

  <VNavigationDrawer
    temporary
    v-model="showDrawer"
    :location="$vuetify.display.mobile ? 'bottom' : undefined"
  >
    <VList lines="one">
      <VListItem
        class="bg-transparent"
        link
        v-for="nav in navItems"
        :key="nav.title"
        :to="nav.to"
        :title="nav.title"
      >
      </VListItem>
    </VList>
  </VNavigationDrawer>
</template>

<style scoped lang="scss">
.status-indicator {
  height: 10px;
  width: 10px;
}
</style>
