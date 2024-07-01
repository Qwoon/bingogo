import { ResourceQuery } from './'

export namespace ListQuery {
  export interface Props extends ResourceQuery.Props {
    limit: string
    offset: string
  }
}
