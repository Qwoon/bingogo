import type { Board } from '../domain';
import type { CrudGateway } from './core';

export interface BoardGateway extends CrudGateway<Board> {
  // empty
}
