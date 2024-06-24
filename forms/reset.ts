export function reset<T extends {}, P extends T>(
  form: T,
  props?: Partial<P>,
  defaultProps?: Partial<P>
): void {
  props = { ...props, ...defaultProps };

  if (props)
    for (let key of Object.keys(form) as Array<keyof T>)
      (form[key] as any) = props[key];
}
