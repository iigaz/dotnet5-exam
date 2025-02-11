import { UserInfoProps } from "../mainPage/userInfo/userInfo.tsx";

function WinnerMessage(props: WinnerMessageProps) {
  if (props.winner === 0) {
    return <>Ничья!</>;
  } else if (
    (props.winner === 1 && props.player1.username === props.currentUser) ||
    (props.winner === 2 && props.player2.username === props.currentUser)
  ) {
    return <>Вы победили!</>;
  } else if (props.winner === 1) {
    return <>{`Победил ${props.player1.username}`}</>;
  } else if (props.winner === 2) {
    return <>{`Победил ${props.player2.username}`}</>;
  }
}

export default WinnerMessage;

export interface WinnerMessageProps {
  currentUser?: string;
  winner: number;
  player1: UserInfoProps;
  player2: UserInfoProps;
}
