import classes from "./Game.module.css";
import Board from "../../components/game/board.tsx";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../../config/axios.ts";
import { UserInfoProps } from "../../components/mainPage/userInfo/userInfo.tsx";
import { AxiosError } from "axios";
import fieldInfoIntoTiles from "../../helpers/fieldInfoIntoTilesArray.ts";

const PLAYER_1 = "x";
const PLAYER_2 = "o";

function Game() {
  const { id } = useParams();
  const navigator = useNavigate();

  const [gameState, setGameState] = useState<GameStateDto | null>(null);
  const [tiles, setTiles] = useState(Array<string | null>(9).fill(null));
  const [playerTurn, setPlayerTurn] = useState(PLAYER_1);

  useEffect(() => {
    api
      .get(`/games/${id}`)
      .then((response) => {
        setGameState(response.data);
        setTiles(fieldInfoIntoTiles(response.data.field));
      })
      .catch((error: AxiosError<any, any>) => {
        if (!error.response) {
          setGameState(null);
        } else if (error.response.status === 401) {
          navigator("/auth");
        }
      });
  }, [id]);

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

export interface GameStateDto {
  player1: UserInfoProps | null;
  player2: UserInfoProps | null;
  field: string;
  turn: number;
}
