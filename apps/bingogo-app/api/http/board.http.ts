import { Board } from '../domain';
import type { BoardGateway } from '../gateway';
import { CrudHttp, type Ctor } from './core';

export class BoardHttp extends CrudHttp<Board> implements BoardGateway {
  protected path: string = '/boards';
  protected ctor: Ctor<Board, any> = Board;
}

export const boardHttp = new BoardHttp();
