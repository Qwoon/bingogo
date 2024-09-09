export const useLoaderStore = defineStore('LoaderStore', {
  state: () => ({
    $container: []
  }),

  getters: {
    container(): string[] {
      return this.$container
    },

    isLoading: (state) => {
      return (component: string) => state.$container.indexOf(component) > -1
    }
  },

  actions: {
    async loadAndAwait(
      component: string,
      process: (...params: any[]) => any | Promise<any>
    ): Promise<boolean> {
      try {
        this.$container.push(component)
        await process()
      } finally {
        const index = this.$container.indexOf(component)
        this.$container.splice(index, 1)
        return true
      }
    }
  }
})
