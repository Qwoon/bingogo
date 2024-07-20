import { Board, boardHttp } from '~/api';

export const useBoardStore = defineStore('BoardStore', () => {
  let resources = ref<Board[]>();

  async function getList(): Promise<void> {
    resources.value = await boardHttp.getBy({
      limit: 100,
      offset: 0,
      fields: [],
    });
  }

  return { resources, getList };
});
