import type { ResourceQuery } from '.';

export namespace ListQuery {
  export interface Props extends ResourceQuery.Props {
    limit: number;
    offset: number;
  }
}
