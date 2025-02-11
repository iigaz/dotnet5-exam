import classes from "./Game.module.css";
import strikeClasses from "../../components/game/strike.module.css";
import Board from "../../components/game/board.tsx";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../../config/axios.ts";
import { UserInfoProps } from "../../components/mainPage/userInfo/userInfo.tsx";
import { AxiosError } from "axios";
import fieldInfoIntoTiles from "../../helpers/fieldInfoIntoTilesArray.ts";
import CustomLoader from "../../components/general/loader/customLoader.tsx";
import useSignalR from "../../hooks/useSignalR.ts";
import Config from "../../config/config.ts";
import { gamesHubClientConnection } from "../../components/game/gamesHubClientConnection.ts";
import WinnerMessage from "../../components/game/winnerMessage.tsx";
import Button from "../../components/general/button/button.tsx";
import exitIcon from "../../assets/exitIcon.svg";

const PLAYER_1 = "x";
const PLAYER_2 = "o";

const winningCombinationForStrike = [
  { combination: [0, 1, 2], strikeClass: strikeClasses.strikeRow1 },
  { combination: [3, 4, 5], strikeClass: strikeClasses.strikeRow2 },
  { combination: [6, 7, 8], strikeClass: strikeClasses.strikeRow3 },

  { combination: [0, 3, 6], strikeClass: strikeClasses.strikeColumn1 },
  { combination: [1, 4, 7], strikeClass: strikeClasses.strikeColumn2 },
  { combination: [2, 5, 8], strikeClass: strikeClasses.strikeColumn3 },

  { combination: [0, 4, 8], strikeClass: strikeClasses.strikeDiagonal1 },
  { combination: [2, 4, 6], strikeClass: strikeClasses.strikeDiagonal2 },
];

function Game() {
  const { id } = useParams();
  const navigator = useNavigate();
  const { connection } = useSignalR(
    Config.BaseURL.replace(/\/$/, "") + "/games/hub",
  );

  const [gameState, setGameState] = useState<GameStateDto | null>(null);
  const [tiles, setTiles] = useState(Array<string | null>(9).fill(null));
  const [gameOver, setGameOver] = useState<boolean>(false);
  const [strikeClass, setStrikeClass] = useState<string>("");

  useEffect(() => {
    api
      .get(`/games/${id}`)
      .then((response) => {
        setGameState(response.data);
        setTiles(fieldInfoIntoTiles(response.data.field));
        setGameOver(false);
      })
      .catch((error: AxiosError<any, any>) => {
        if (!error.response) {
          //TODO редирект на main page?

          setGameState(null);
          const newTiles = [...tiles];
          newTiles.fill(null);
          setTiles(newTiles);
        } else if (error.response.status === 401) {
          navigator("/auth");
        }
      });

    if (!connection) return;
    const gameClient = gamesHubClientConnection(connection);
    gameClient.send.Join(id!);
  }, [id]);

  useEffect(() => {
    if (!connection) return;
    const gameClient = gamesHubClientConnection(connection);
    return gameClient.on.ReceiveGameState((gameState) => {
      setGameState(gameState);
      setGameOver(false);
      setTiles(fieldInfoIntoTiles(gameState.field));
    });
  }, [connection]);

  useEffect(() => {
    if (!connection) return;
    const gameClient = gamesHubClientConnection(connection);
    return gameClient.on.ReceiveDeclaredWinner((winnerDeclaration) => {
      setGameState({
        ...gameState!,
        turn: winnerDeclaration.winner,
        player1: winnerDeclaration.player1,
        player2: winnerDeclaration.player2,
      });
      setGameOver(true);
      for (const { combination, strikeClass } of winningCombinationForStrike) {
        const tileValue1 = tiles[combination[0]];
        const tileValue2 = tiles[combination[1]];
        const tileValue3 = tiles[combination[2]];

        if (
          tileValue1 !== null &&
          tileValue1 === tileValue2 &&
          tileValue2 === tileValue3
        ) {
          setStrikeClass(strikeClass);
        }
      }
    });
  }, [connection]);

  const handleTitleClick = (index: number) => {
    if (tiles[index] !== null) {
      return;
    }

    if (!connection) return;
    const gamesClient = gamesHubClientConnection(connection);

    const newTiles = [...tiles];
    newTiles[index] = gameState?.turn == 1 ? PLAYER_1 : PLAYER_2;
    gamesClient.send.PlaceMark(id!, index % 3, Math.floor(index / 3));
    setTiles(newTiles);
  };

  const handleExit = () => {
    if (!connection) return;
    const gamesClient = gamesHubClientConnection(connection);
    gamesClient.send.Leave(id!);
    navigator("/games");
  };

  return (
    <div className={classes.container}>
      {gameState === null ? (
        <CustomLoader />
      ) : (
        <div className={classes.infoHeader}>
          <div className={classes.userInfo}>
            {gameState.player1 === null || gameState.player1 === undefined ? (
              <>
                <p>Игрок1</p>
                <p>ожидается</p>
              </>
            ) : (
              <>
                <p>{gameState.player1.username}</p>
                <p>{`Рейтинг ${gameState.player1.rating}`}</p>
              </>
            )}
          </div>
          <h1>
            {gameOver ? (
              <WinnerMessage
                winner={gameState.turn}
                player1={gameState.player1!}
                player2={gameState.player2!}
              />
            ) : (
              <>
                {gameState.turn == 1 ? (
                  <>Ваш ход за крестик</>
                ) : (
                  <>{`${gameState.player2!.username}`} ходит за нолик</>
                )}
              </>
            )}
          </h1>
          <div
            className={[classes.userInfo, classes.textAlignEnd]
              .join(" ")
              .trim()}
          >
            {gameState.player2 === null || gameState.player2 === undefined ? (
              <>
                <p>Игрок2</p>
                <p>ожидается</p>
              </>
            ) : (
              <>
                <p>{gameState.player2.username}</p>
                <p>{`Рейтинг ${gameState.player2.rating}`}</p>
              </>
            )}
          </div>
        </div>
      )}

      <Board
        handleTileClick={handleTitleClick}
        tileStates={tiles}
        strikeClass={strikeClass}
      />
      <div className={classes.exitButtonContainer}>
        <Button onClick={handleExit}>
          <div className={classes.exitButton}>
            <span>Выйти</span>
            <img src={exitIcon} className={classes.exitIcon} />
          </div>
        </Button>
      </div>
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

export interface WinnerDeclarationDto {
  winner: number;
  player1: UserInfoProps;
  player2: UserInfoProps;
}

const GameStateMock: GameStateDto = {
  player1: {
    username: "user1user1",
    rating: 1000,
  },
  player2: null,
  field: "-x--o--xx",
  turn: 1,
};
