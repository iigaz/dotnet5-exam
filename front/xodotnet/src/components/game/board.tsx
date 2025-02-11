import classes from "./board.module.css";
import Tile from "./tile.tsx";

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
    </div>
  );
}

export default Board;

export interface BoardProps {
  tileStates: Array<string | null>;
  handleTileClick: (index: number) => void;
}
