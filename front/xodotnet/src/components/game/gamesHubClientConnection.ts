import { HubConnection } from "@microsoft/signalr";
import { UserInfoProps } from "../mainPage/userInfo/userInfo.tsx";

export function gamesHubClientConnection(connection: HubConnection) {
  const onMethod = (
    method: string,
    actionsOnReceive: (...args: any[]) => any,
  ) => {
    connection.on(method, actionsOnReceive);
    return () => connection.off(method);
  };
  return {
    on: {
      ReceiveGameState: (
        actionsOnReceive: (
          player1: UserInfoProps | null,
          player2: UserInfoProps | null,
          field: string,
          turn: number,
        ) => void,
      ) => onMethod("UpdateState", actionsOnReceive),
      ReceiveDeclaredWinner: (
        actionsOnReceive: (
          winner: number,
          player1: UserInfoProps,
          player2: UserInfoProps,
        ) => void,
      ) => onMethod("DeclareWinner", actionsOnReceive),
    },
    send: {
      PlaceMark: (gameId: string, x: number, y: number) => {
        connection.send("PlaceMark", gameId, x, y);
      },
      Spectate: (gameId: string) => {
        connection.send("Spectate", gameId);
      },
      Join: (gameId: string) => {
        connection.send("Join", gameId);
      },
      Leave: (gameId: string) => {
        connection.send("Leave", gameId);
      },
    },
  };
}
