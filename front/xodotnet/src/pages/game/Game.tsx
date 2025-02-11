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
import getUsernameFromAccessToken from "../../helpers/getDecodedAccessToken.ts";
import TurnMessage from "../../components/game/turnMessage.tsx";

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
  const [currentUser, setCurrentUser] = useState<string>();

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
          setGameState(null);
          const newTiles = [...tiles];
          newTiles.fill(null);
          setTiles(newTiles);
          navigator("/games");
        } else if (error.response.status === 401) {
          navigator("/auth");
        }
      });
    setCurrentUser(getUsernameFromAccessToken());
  }, [id]);

  useEffect(() => {
    if (!connection) {
      return;
    }
    const gameClient = gamesHubClientConnection(connection);
    gameClient.send.Spectate(id!);
  }, [connection]);

  useEffect(() => {
    if (!connection) {
      return;
    }
    const gameClient = gamesHubClientConnection(connection);
    return gameClient.on.ReceiveGameState(
      (
        field: string,
        turn: number,
        player1: UserInfoProps | null,
        player2: UserInfoProps | null,
      ) => {
        setGameState({
          player1: player1,
          player2: player2,
          field: field,
          turn: turn,
        });
        setGameOver(false);
        setStrikeClass("");
        setTiles(fieldInfoIntoTiles(field));
      },
    );
  }, [connection]);

  useEffect(() => {
    if (!connection) return;
    const gameClient = gamesHubClientConnection(connection);
    return gameClient.on.ReceiveDeclaredWinner(
      (winner: number, player1: UserInfoProps, player2: UserInfoProps) => {
        setGameState({
          ...gameState!,
          turn: winner,
          player1: player1,
          player2: player2,
        });
        setGameOver(true);
      },
    );
  }, [connection]);

  useEffect(() => {
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
  }, [gameOver]);

  const handleTitleClick = (index: number) => {
    if (tiles[index] !== null) {
      return;
    }

    if (!connection) return;
    const gamesClient = gamesHubClientConnection(connection);
    gamesClient.send.PlaceMark(id!, index % 3, Math.floor(index / 3));
  };

  const handleJoin = () => {
    if (!connection) return;
    const gameClient = gamesHubClientConnection(connection);
    gameClient.send.Join(id!);
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
                currentUser={currentUser}
                winner={gameState.turn}
                player1={gameState.player1!}
                player2={gameState.player2!}
              />
            ) : (
              <TurnMessage
                currentUser={currentUser}
                {...gameState}
              ></TurnMessage>
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
      <div className={classes.joinButtonContainer}>
        <Button onClick={handleJoin}>
          <div className={classes.joinButton}>
            <span>Присоединиться</span>
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
