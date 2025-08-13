import type { ListQuery } from './list-query'

export declare namespace BoardQuery {
  export interface Props extends ListQuery.Props {
    ids: number[]
    categories: string[]
    gameMode: number
    gameType: number
  }
}
