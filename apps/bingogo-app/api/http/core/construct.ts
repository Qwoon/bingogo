import type { Ctor } from './crud.http';

export function construct<T>(ctor: Ctor<T>, props: object): T {
  return Reflect.construct(ctor, [props]);
}
