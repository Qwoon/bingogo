import type { BoardTile } from '~/domain'
import { reset } from './reset'

export interface BoardTileForm extends BoardTile.Props {
  // empty
}

export namespace BoardTileForm {
  export function create(props?: Partial<BoardTile>): BoardTileForm {
    const form: BoardTileForm = {
      title: '',
      isChecked: false,
      points: 0,
      boardId: 0
    }

    reset(form, props)

    return form
  }
}
