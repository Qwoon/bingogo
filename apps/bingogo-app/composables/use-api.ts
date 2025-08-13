import type { Board, BoardQuery, BoardTile, ResourceQuery } from '~/domain'
import type { BoardTileQuery } from '~/domain/queries/board-tile-query'
import type { BoardForm, BoardTileForm } from '~/forms'

export const useApi = () => {
  return {
    // Boards
    getBoards(query: ComputedRef<Partial<BoardQuery.Props>>) {
      return useFetch<Board.Props[]>(() => `/boards`, {
        query: query,
        baseURL: useApiBase()
      })
    },

    getBoardById(id: number, query?: ResourceQuery.Props) {
      return useFetch<Board.Props>(() => `/boards/${id}`, {
        query: query,
        baseURL: useApiBase()
      })
    },

    createBoard(forms: BoardForm[]) {
      return $fetch(`/boards`, {
        method: 'POST',
        body: forms,
        baseURL: useApiBase()
      })
    },

    updateBoard(id: number, props: Board.Props) {
      return $fetch(`/boards/${id}`, {
        method: 'PUT',
        body: props,
        baseURL: useApiBase()
      })
    },

    deleteBoard(id: number) {
      return $fetch(`/boards/${id}`, {
        method: 'DELETE',
        baseURL: useApiBase()
      })
    },

    // Board tiles
    getBoardTiles(query: ComputedRef<Partial<BoardTileQuery.Props>>) {
      return useFetch<Board.Props[]>(() => `/board-tiles`, {
        query: query,
        baseURL: useApiBase()
      })
    },

    getBoardTileById(id: number, query?: ResourceQuery.Props) {
      return useFetch<Board.Props>(() => `/board-tiles/${id}`, {
        query: query,
        baseURL: useApiBase()
      })
    },

    createBoardTile(forms: BoardTileForm[]) {
      return $fetch(`/board-tiles`, {
        method: 'POST',
        body: forms,
        baseURL: useApiBase()
      })
    },

    updateBoardTile(id: number, props: BoardTile.Props) {
      return $fetch(`/board-tiles/${id}`, {
        method: 'PUT',
        body: props,
        baseURL: useApiBase()
      })
    },

    updateBoardTiles(props: { [id: number]: BoardTile.Props }) {
      return $fetch(`/board-tiles`, {
        method: 'PUT',
        body: props,
        baseURL: useApiBase()
      })
    },

    deleteBoardTile(id: number) {
      return $fetch(`/board-tiles/${id}`, {
        method: 'DELETE',
        baseURL: useApiBase()
      })
    }
  }
}
