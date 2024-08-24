import type { BoardTile } from '../domain';
import type { CrudGateway } from './core';

export interface BoardTileGateway extends CrudGateway<BoardTile> {
  // empty
}
