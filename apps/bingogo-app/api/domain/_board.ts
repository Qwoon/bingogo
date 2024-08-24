import type { BoardTile } from './_board-tile';
import { Resource, type Historical } from './core';

export declare namespace Board {
  export interface Props extends Resource.Props, Historical.Props {
    title: string;
    isPublic: boolean;
    hasEvents: boolean;
    gameMode: number;
    gameType: number;
    category: string;
    tiles: BoardTile.Props[];
  }
}

export interface Board extends Readonly<Board.Props> {
  // empty
}

export class Board extends Resource<Board.Props> {
  // empty
}
