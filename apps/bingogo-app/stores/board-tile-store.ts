import { BoardTile, construct } from '~/domain'
import type { BoardTileForm } from '~/forms'

export const useBoardTileStore = defineStore('BoardTileStore', () => {
  const path: string = '/board-tiles'
  let resource = ref<BoardTile | null>()
  let resources = ref<BoardTile[] | null>()

  async function create(forms: BoardTileForm[]): Promise<BoardTile[]> {
    const { data } = await useAsyncData(
      () =>
        $fetch(`${useRuntimeConfig().public.apiBase}${path}`, {
          method: 'POST',
          body: forms
        }),
      {
        transform: (data: BoardTile[]) => {
          const res = new Array<BoardTile>()
          for (const item of JSON.parse(JSON.stringify(data))) res.push(construct(BoardTile, item))

          return res
        }
      }
    )

    return data.value
  }

  async function update(props: { [id: number]: BoardTile.Props }): Promise<BoardTile[]> {
    const { data } = await useAsyncData(
      () =>
        $fetch(`${useRuntimeConfig().public.apiBase}${path}`, {
          method: 'PUT',
          body: props
        }),
      {
        transform: (data: BoardTile[]) => {
          const res = new Array<BoardTile>()
          for (const item of JSON.parse(JSON.stringify(data))) res.push(construct(BoardTile, item))

          return res
        }
      }
    )

    return data.value
  }

  async function remove(id: number): Promise<BoardTile> {
    const { data } = await useAsyncData(
      () =>
        $fetch(`${useRuntimeConfig().public.apiBase}${path}/${id}`, {
          method: 'DELETE'
        }),
      {
        transform: (data: BoardTile) => {
          return construct(BoardTile, data)
        }
      }
    )

    return data.value
  }

  return { create, update, remove }
})
