export const useGameStore = defineStore('GameStore', () => {
  let currentGameName = ref<string>('Bingogo');

  return { currentGameName };
});
