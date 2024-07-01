import type { BoardTile } from './board-tile';
import type { Historical, Resource } from './core';

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
