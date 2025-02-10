import classes from "./MainPage.module.css";
import Button from "../../components/general/button/button.tsx";
import GamesList, {
  GamesListProps,
} from "../../components/gamesList/gamesList.tsx";

function MainPage() {
  return (
    <div className={classes.container}>
      <div className={classes.gameAndRating}>
        <h1>Tic Tac Toe</h1>
        <Button>Рейтинг</Button>
        <Button>Создать игру</Button>
      </div>
      <GamesList {...tempList} />
    </div>
  );
}

export default MainPage;

const tempList: GamesListProps = {
  page: 1,
  maxPage: 5,
  content: [
    {
      id: "17d289aa-32d3-40af-bcc9-65272a6a692c",
      status: "open",
      createdAt: "10.02.2025 19:30:00",
      maxRating: 10,
      creator: "string",
    },
    {
      id: "17d289aa-32d3-40af-bcc9-65272a6a692c",
      status: "ongoing",
      createdAt: "10.02.2025 19:30:00",
      maxRating: 10,
      creator: "string",
    },
    {
      id: "17d289aa-32d3-40af-bcc9-65272a6a692c",
      status: "ongoing",
      createdAt: "10.02.2025 19:30:00",
      maxRating: 10,
      creator: "string",
    },
    {
      id: "17d289aa-32d3-40af-bcc9-65272a6a692c",
      status: "completed",
      createdAt: "10.02.2025 19:30:00",
      maxRating: 10,
      creator: "string",
    },
  ],
};
