import { clear } from './'

export function copy<T, P>(object: T, source: P): T & P {
  if (Object.is(object, source)) throw new Error('Cannot copy properties of self')

  clear(object)

  if (source)
    for (const key of Object.keys(source) as Array<keyof (T | P)>) object[key] = source[key] as any

  return object as T & P
}
