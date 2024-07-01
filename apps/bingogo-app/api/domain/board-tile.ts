import type { Resource } from './core';

export declare namespace BoardTile {
  export interface Props extends Resource.Props {
    title: string;
    isChecked: boolean;
    points: number;
    boardId: number;
  }
}

export interface BoardTile extends Readonly<BoardTile.Props> {
  // empty
}
