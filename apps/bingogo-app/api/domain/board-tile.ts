import { Resource } from './core';

export declare namespace BoardTile {
  export interface Props extends Resource.Props {
    title: string;
    isChecked: boolean;
    points: number;
    boardId: number;
    isMock?: boolean;
  }
}

export interface BoardTile extends Readonly<BoardTile.Props> {
  // empty
}

export class BoardTile extends Resource<BoardTile.Props> {
  static constructEmpty = (): BoardTile =>
    new BoardTile({
      title: null,
      isChecked: true,
      points: 0,
      boardId: null,
      isMock: true,
    });
}
