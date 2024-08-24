export type Ctor<R = any, T extends any[] = any> = new (...args: T) => R;

export function construct<T>(ctor: Ctor<T>, props: object): T {
  return Reflect.construct(ctor, [props]);
}
