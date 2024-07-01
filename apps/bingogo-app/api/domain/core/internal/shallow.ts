export function shallow<T>(object: T): T {
  if (object != null) {
    if (Array.isArray(object)) return [...object] as any
    if (object instanceof Date) return object
    if (typeof object === 'object') return { ...object }
  }
  return object
}
