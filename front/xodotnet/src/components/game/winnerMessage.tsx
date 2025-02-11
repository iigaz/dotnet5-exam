import { WinnerDeclarationDto } from "../../pages/game/Game.tsx";

function WinnerMessage(props: WinnerDeclarationDto) {
  if (props.winner === 0) return <>Ничья</>;
  else if (props.winner === 1) return <>{`Победил ${props.player1}`}</>;
  else if (props.winner === 2) return <>{`Победил ${props.player2}`}</>;
}

export default WinnerMessage;
