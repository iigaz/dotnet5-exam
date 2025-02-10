import classes from "./gamesList.module.css";
import GameInfoCard, { GameInfoCardProps } from "./gameInfoCard.tsx";

function GamesList(props: GamesListProps) {
  return (
    <div className={classes.listWrapper}>
      {props.content.map((infoCard: GameInfoCardProps, i: number) => (
        <GameInfoCard {...infoCard} key={i} />
      ))}
    </div>
  );
}

export default GamesList;

export interface GamesListProps {
  page: number;
  maxPage: number;
  content: Array<GameInfoCardProps>;
}
