<script setup lang="ts">
const notificationStore = useNotificationStore()
const { hasNotification, text, notificationType } = storeToRefs(notificationStore)

const snackbarColorResolver = computed(() => (type: NotificationType | null) => {
  switch (type) {
    case 'error':
      return 'error'
    case 'info':
      return 'primary'
    case 'warning':
      return 'warning'
    default:
      return 'info'
  }
})
</script>

<template>
  <VApp>
    <VMain>
      <VSnackbar :color="snackbarColorResolver(notificationType)" v-model="hasNotification"
        >{{ text }}
      </VSnackbar>
      <NuxtPage />
    </VMain>
  </VApp>
</template>
