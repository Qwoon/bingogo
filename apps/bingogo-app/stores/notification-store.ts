import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

export type NotificationType = 'info' | 'warning' | 'error'

export const useNotificationStore = defineStore('NotificationStore', () => {
  const text = ref<string | null>(null)
  const notificationType = ref<NotificationType | null>(null)

  const hasNotification = computed(() => !!text.value)

  const setMessage = (
    value: string,
    type: NotificationType = 'info',
    timeout: number = 10000
  ): void => {
    text.value = value
    notificationType.value = type
    setTimeout(() => {
      $reset()
    }, timeout)
  }

  function $reset(): void {
    text.value = null
    notificationType.value = null
  }

  return { text, hasNotification, notificationType, setMessage, $reset }
})
