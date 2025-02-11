import classes from "./gameInfoCard.module.css";
import { UUID } from "crypto";
import Button from "../general/button/button.tsx";
import { Link } from "react-router-dom";
import { FC } from "react";

function GameInfoCard(props: GameInfoCardProps) {
  return (
    <div className={classes.cardContainer}>
      <div className={classes.leftSideCardInfo}>
        <p>{props.creator}</p>
        <p>{new Date(props.createdAt).toLocaleString()}</p>
        <p>{props.id}</p>
      </div>
      <div className={classes.rightSideCardInfo}>
        <p>
          {"Макс. рейтинг не более "}
          {props.maxRating}
        </p>
        <StatusComponent status={props.status} />
      </div>
      <div className={classes.buttonWrapper}>
        {props.status == "ongoing" ? (
          <Link to={`/games/${props.id}`}>
            <Button>Наблюдать</Button>
          </Link>
        ) : (
          <Link to={`/games/${props.id}`}>
            <Button disabled={props.status == "completed"}>Играть</Button>
          </Link>
        )}
      </div>
    </div>
  );
}

export default GameInfoCard;

type Status = "open" | "ongoing" | "completed";

export interface GameInfoCardProps {
  id: UUID;
  status: Status;
  createdAt: string;
  maxRating: number;
  creator: string;
}

const statusMap: Record<Status, string> = {
  open: "Открыта",
  ongoing: "Начата",
  completed: "Завершена",
};

const StatusComponent: FC<{ status: Status }> = ({ status }) => {
  return <p>Статус игры: {statusMap[status]}</p>;
};
