import classes from "./gameInfoCard.module.css";
import { UUID } from "crypto";
import Button from "../general/button/button.tsx";

function GameInfoCard(props: GameInfoCardProps) {
  return (
    <div className={classes.cardContainer}>
      <div className={classes.leftSideCardInfo}>
        <p>{props.creator}</p>
        <p>{props.createdAt}</p>
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
          <Button>Наблюдать</Button>
        ) : (
          <Button disabled={props.status == "completed"}>Играть</Button>
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

const StatusComponent: React.FC<{ status: Status }> = ({ status }) => {
  return <p>Статус игры: {statusMap[status]}</p>;
};
