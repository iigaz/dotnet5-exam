import classes from "./Game.module.css";
import Board from "../../components/game/board.tsx";
import { useState } from "react";

const PLAYER_1 = "x";
const PLAYER_2 = "o";

function Game() {
  const [tiles, setTiles] = useState(Array<string | null>(9).fill(null));
  const [playerTurn, setPlayerTurn] = useState(PLAYER_1);

  const handleTitleClick = (index: number) => {
    if (tiles[index] !== null) {
      return;
    }

    const newTiles = [...tiles];
    newTiles[index] = playerTurn;
    setTiles(newTiles);

    if (playerTurn === PLAYER_1) {
      setPlayerTurn(PLAYER_2);
    } else {
      setPlayerTurn(PLAYER_1);
    }
  };

  return (
    <div className={classes.container}>
      <div>
        <h1>Ваш ход</h1>
      </div>
      <Board handleTileClick={handleTitleClick} tileStates={tiles} />
    </div>
  );
}

export default Game;
