import { array, boolean, object, string, type AnySchema } from 'yup'
import type { Board, BoardTile } from '~/domain'
import { reset } from './reset'

export interface BoardForm extends Board.Props {
  allowMultiplayer: boolean
}

export const boardValidationSchema = object<Partial<Record<keyof BoardForm, AnySchema>>>({
  title: string().required('Board title is required'),
  allowMultiplayer: boolean().optional(),
  tiles: array()
    .of(
      object<BoardTile.Props>().shape({
        title: string().required('Tile title is required')
      })
    )
    .min(1)
})

export namespace BoardForm {
  export function create(props?: Partial<BoardForm>): BoardForm {
    const form: BoardForm = {
      title: '',
      tiles: [],
      allowMultiplayer: false,
      isPublic: false,
      hasEvents: false,
      gameMode: 0,
      gameType: 0,
      category: '',
      createdById: 0,
      updatedById: 0,
      deletedById: 0,
      createdAt: undefined,
      updatedAt: undefined,
      deletedAt: undefined
    }

    reset(form, props)

    return form
  }
}
