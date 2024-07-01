import { type ListQuery, Resource, type ResourceQuery } from '@/api/domain';

export interface CrudGateway<T extends Resource, ID = number> {
  getById(id: ID, query?: ResourceQuery.Props): Promise<T>;
  getBy(query: ListQuery.Props): Promise<T[]>;
  update(id: ID, object: T): Promise<T>;
  post(objects: T[]): Promise<T[]>;
  delete(id: ID): Promise<T>;
}
