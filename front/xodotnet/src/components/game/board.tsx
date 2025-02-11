import classes from "./board.module.css";
import Tile from "./tile.tsx";
import Strike from "./strike.tsx";

function Board(props: BoardProps) {
  return (
    <div className={classes.board}>
      {props.tileStates.map((tileState, index: number) => (
        <Tile
          onTileClick={() => props.handleTileClick(index)}
          tileValue={tileState === null ? "" : tileState}
          rightBorder={index % 3 != 2}
          bottomBorder={index < 6}
        />
      ))}
      <Strike strikeClass={props.strikeClass} />
    </div>
  );
}

export default Board;

export interface BoardProps {
  strikeClass: string;
  tileStates: Array<string | null>;
  handleTileClick: (index: number) => void;
}
