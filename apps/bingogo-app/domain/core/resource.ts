import { copy, shallow } from '.'

export declare namespace Resource {
  export interface Props {
    id?: number
  }
}

export abstract class Resource<P = Resource.Props> {
  constructor(source?: Partial<P>) {
    const props = this.adjust(source) as any
    copy(this, props)
  }

  private adjust(source: Partial<P>): P {
    const object = {} as P

    if (source)
      for (const key of Object.keys(source) as Array<keyof P>) object[key] = shallow(source[key])

    return object
  }
}
