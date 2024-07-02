import { BoardTile } from '../domain';
import type { BoardTileGateway } from '../gateway';
import { CrudHttp, type Ctor } from './core';

export class BoardTileHttp
  extends CrudHttp<BoardTile>
  implements BoardTileGateway
{
  protected path: string = '/board-tiles';
  protected ctor: Ctor<BoardTile, any> = BoardTile;
}

export const boardTileHttp = new BoardTileHttp();
