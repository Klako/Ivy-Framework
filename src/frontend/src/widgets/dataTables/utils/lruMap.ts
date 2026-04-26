/**
 * Insertion-ordered Map with a max size; evicts least-recently-used entries.
 */
export class LruMap<K, V> {
  private readonly map = new Map<K, V>();

  constructor(private readonly maxSize: number) {}

  get(key: K): V | undefined {
    const value = this.map.get(key);
    if (value !== undefined) {
      this.map.delete(key);
      this.map.set(key, value);
    }
    return value;
  }

  set(key: K, value: V): void {
    if (this.map.has(key)) this.map.delete(key);
    this.map.set(key, value);
    while (this.map.size > this.maxSize) {
      const first = this.map.keys().next().value;
      if (first !== undefined) this.map.delete(first);
    }
  }

  get size(): number {
    return this.map.size;
  }
}
