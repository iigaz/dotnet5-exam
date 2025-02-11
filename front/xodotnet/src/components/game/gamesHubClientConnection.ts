import { HubConnection } from "@microsoft/signalr";
import { GameStateDto, WinnerDeclarationDto } from "../../pages/game/Game.tsx";

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
      ReceiveGameState: (actionsOnReceive: (gameState: GameStateDto) => void) =>
        onMethod("UpdateState", actionsOnReceive),
      ReceiveDeclaredWinner: (
        actionsOnReceive: (winner: WinnerDeclarationDto) => void,
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
