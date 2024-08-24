export function clear(object: any): void {
  if (object) for (const key of Object.keys(object)) delete object[key]
  return object
}
