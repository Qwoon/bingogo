import { Board, construct, type ResourceQuery } from '~/domain'
import type { BoardForm } from '~/forms'

export const BOARDS_COMPONENT_LOADER_KEY = 'BOARDS_COMPONENT_LOADER_KEY'
export const BOARD_COMPONENT_LOADER_KEY = 'BOARD_COMPONENT_LOADER_KEY'

export const useBoardStore = defineStore('BoardStore', () => {
  const path = '/boards'
  let resource = ref<Board | null>()
  let resources = ref<Board[] | null>()

  async function get(id: number, query?: Partial<ResourceQuery.Props>): Promise<Board> {
    const { data } = await useFetch(`${useRuntimeConfig().public.apiBase}${path}/${id}`, {
      query: {
        query
      },
      transform: (data: Board) => {
        return construct(Board, data)
      }
    })

    resource.value = data.value
    return resource.value
  }

  async function getList(): Promise<Board[]> {
    const { data } = await useFetch(`${useRuntimeConfig().public.apiBase}${path}`, {
      transform: (data: Board[]) => {
        const res = new Array<Board>()
        for (const item of JSON.parse(JSON.stringify(data))) res.push(construct(Board, item))

        return res
      }
    })

    resources.value = data.value
    return resources.value
  }

  async function create(forms: BoardForm[]): Promise<Board[]> {
    const { data, error } = await useAsyncData(
      () =>
        $fetch(`${useRuntimeConfig().public.apiBase}${path}`, {
          method: 'POST',
          body: forms
        }),
      {
        transform: (data: Board[]) => {
          const res = new Array<Board>()
          for (const item of JSON.parse(JSON.stringify(data))) res.push(construct(Board, item))

          return res
        }
      }
    )

    return data.value
  }

  async function update(id: number, props: Board.Props): Promise<Board> {
    const { data } = await useAsyncData(
      () =>
        $fetch(`${useRuntimeConfig().public.apiBase}${path}/${id}`, {
          method: 'PUT',
          body: props
        }),
      {
        transform: (data: Board) => {
          return construct(Board, data)
        }
      }
    )

    return data.value
  }

  return { resource, resources, get, getList, create, update }
})
