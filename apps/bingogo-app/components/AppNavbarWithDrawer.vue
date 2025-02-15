<script setup lang="ts">
import { useDisplay } from 'vuetify'

const store = useGameStore()
const { currentGameName } = storeToRefs(store)

const navItems = [
  {
    title: 'Boards',
    to: { name: 'index' }
  },
  {
    title: 'Create board',
    to: { name: 'creator' }
  }
]

const mdAndUp = useDisplay().mdAndUp
const showDrawer = ref<boolean>(mdAndUp.value ? true : false)
</script>

<template>
  <VAppBar scroll-behavior="" elevation="0" scroll-threshold="2">
    <template #title v-if="$vuetify.display.mdAndUp">
      <h5 class="text-h5">
        {{ currentGameName }}
      </h5>
    </template>

    <template #default>
      <VSpacer />
    </template>

    <template #append>
      <VBtn append-icon="mdi-login" variant="elevated"> Login </VBtn>
    </template>
  </VAppBar>

  <VNavigationDrawer v-model="showDrawer" :width="mdAndUp ? 150 : 175">
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
