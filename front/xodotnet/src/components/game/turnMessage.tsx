import { UserInfoProps } from "../mainPage/userInfo/userInfo.tsx";

function TurnMessage(props: TurnMessageProps) {
  if (props.player1 === null || props.player2 === null) {
    return <>Ожидание противника</>;
  } else if (props.turn == 1 && props.player1.username === props.currentUser) {
    return <>Ваш ход за крестик</>;
  } else if (props.turn === 1 && props.player2.username === props.currentUser) {
    return <>{`${props.player1!.username}`} ходит за крестик</>;
  } else if (props.turn == 2 && props.player2.username === props.currentUser) {
    return <>Ваш ход за нолик</>;
  } else if (props.turn === 2 && props.player1.username === props.currentUser) {
    return <>{`${props.player2!.username}`} ходит за нолик</>;
  }
}

export default TurnMessage;

export interface TurnMessageProps {
  currentUser?: string;
  player1: UserInfoProps | null;
  player2: UserInfoProps | null;
  field: string;
  turn: number;
}
