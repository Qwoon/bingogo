export const useGameStore = defineStore('GameStore', () => {
  let currentGameName = ref<string>('Papich Bingo');

  return { currentGameName };
});
