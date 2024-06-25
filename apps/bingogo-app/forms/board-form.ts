import { array, boolean, object, string, type AnySchema } from 'yup';
import type { Tile } from '~/apps/bingogo-app/api';
import { reset } from './reset';

export interface BoardForm {
  name: string;
  allowMultiplayer: boolean;
  tiles: Tile.Props[];
}

export const boardValidationSchema = object<
  Partial<Record<keyof BoardForm, AnySchema>>
>({
  name: string().required('Board name is required'),
  allowMultiplayer: boolean().optional(),
  tiles: array()
    .of(
      object<Tile.Props>().shape({
        title: string().required('Tile title is required'),
      })
    )
    .min(1),
});

export namespace BoardForm {
  export function create(props?: Partial<BoardForm>): BoardForm {
    const form: BoardForm = {
      name: '',
      tiles: [],
      allowMultiplayer: false,
    };

    reset(form, props);

    return form;
  }
}
