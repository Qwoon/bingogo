import type { ListQuery } from './list-query'

export declare namespace BoardTileQuery {
  export interface Props extends ListQuery.Props {
    ids: number[]
    boardId: number
  }
}
