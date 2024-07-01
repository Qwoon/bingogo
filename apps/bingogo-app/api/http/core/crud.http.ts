import { type ListQuery, Resource, type ResourceQuery } from '@/api/domain';
import type { CrudGateway } from '@/api/gateway/core';
import { HttpGateway } from './http.gateway';

export type Ctor<R = any, T extends any[] = any> = new (...args: T) => R;

export abstract class CrudHttp<T extends Resource, ID = number>
  extends HttpGateway
  implements CrudGateway<T, ID>
{
  protected abstract ctor: Ctor<T>;

  protected unwrap = (data: any): T => this.construct(JSON.parse(data));
  protected unwrapList = (data: any): T[] => {
    const res = new Array<T>();
    for (const item of JSON.parse(data)) res.push(this.construct(item));

    return res;
  };

  async getById(id: ID, query?: ResourceQuery.Props): Promise<T> {
    if (id) {
      const res = await this.axiosClient.get<T>(
        `${this.path}/${id.toString()}`,
        {
          params: query,
          paramsSerializer: { indexes: null },
          transformResponse: (data) => this.unwrap(data),
        }
      );

      return res.data;
    }

    throw new TypeError('Parameter type cannot be null');
  }

  async getBy(query: ListQuery.Props): Promise<T[]> {
    const res = await this.axiosClient.get<T[]>(`${this.path}`, {
      params: query,
      paramsSerializer: { indexes: null },
      transformResponse: (data) => this.unwrapList(data),
    });

    return res.data;
  }

  async update(id: ID, object: T): Promise<T> {
    if (id && object) {
      return await this.axiosClient.put(`${this.path}/${id}`, object, {
        transformResponse: (data) => this.unwrap(data),
      });
    }

    throw Error('Id and objects must be a value.');
  }

  async post(objects: T[]): Promise<T[]> {
    if (objects) {
      return await this.axiosClient.post(`${this.path}`, objects, {
        transformResponse: (data) => this.unwrapList(data),
      });
    }

    throw Error('Objects must be a value.');
  }

  async delete(id: ID): Promise<T> {
    if (id) {
      return await this.axiosClient.delete(`${this.path}/${id}`, {
        transformResponse: (data) => this.unwrap(data),
      });
    }

    throw Error('Id must be a value.');
  }

  protected construct(props: object): T {
    if (props === null) throw Error('Failed to construct type.');
    return Reflect.construct(this.ctor, [props]);
  }
}
