export const useGameStore = defineStore('GameStore', () => {
  //const boardStore = useBoardStore();
  const router = useRouter()
  const currentGameName = computed(() => {
    if (router.currentRoute.value.name === 'board-id')
      // return boardStore.resource ? boardStore.resource.title : 'Bingogo';
      return 'Bingogo'
    return 'Bingogo'
  })

  return { currentGameName }
})
