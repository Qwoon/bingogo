<script setup lang="ts">
const notificationStore = useNotificationStore()
const { hasNotification, text, notificationType, snackbarColorResolver } =
  storeToRefs(notificationStore)
</script>
<template>
  <VApp>
    <VSnackbar :color="snackbarColorResolver(notificationType)" v-model="hasNotification">{{
      text
    }}</VSnackbar>
    <AppNavbar />
    <VMain>
      <NuxtPage />
    </VMain>
  </VApp>
</template>
