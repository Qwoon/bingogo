import { Board, construct } from '~/api';

export const useBoardStore = defineStore('BoardStore', () => {
  let resources = ref<Board[] | null>();

  async function getList(): Promise<Board[]> {
    const { data } = await useFetch(
      `${useRuntimeConfig().public.apiBase}api/boards`,
      {
        transform: (data: Board[]) => {
          const res = new Array<Board>();
          for (const item of JSON.parse(JSON.stringify(data)))
            res.push(construct(Board, item));

          return res;
        },
      }
    );

    resources.value = data.value;

    return resources.value;
  }

  return { resources, getList };
});
