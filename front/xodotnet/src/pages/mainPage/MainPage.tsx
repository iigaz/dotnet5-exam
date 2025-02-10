import classes from "./MainPage.module.css";
import Button from "../../components/general/button/button.tsx";
import GamesList, {
  GamesListProps,
} from "../../components/gamesList/gamesList.tsx";
import RatingModalWindow from "../../components/mainPageModalWindows/ratingModalWindow/ratingModalWindow.tsx";
import { useState } from "react";
import CreateNewGameModalWindow from "../../components/mainPageModalWindows/createNewGameModalWindow/createNewGameModalWindow.tsx";

function MainPage() {
  const [ratingWindowOpen, setRatingWindowOpen] = useState(false);
  const [createGameWindowOpen, setCreateGameWindowOpen] = useState(false);

  return (
    <div className={classes.container}>
      <div className={classes.gameAndRating}>
        <h1>Tic Tac Toe</h1>
        <Button onClick={() => setRatingWindowOpen(true)}>Рейтинг</Button>
        <Button onClick={() => setCreateGameWindowOpen(true)}>
          Создать игру
        </Button>
      </div>
      <GamesList {...tempList} />
      <RatingModalWindow
        open={ratingWindowOpen}
        onClose={() => setRatingWindowOpen(false)}
      />
      <CreateNewGameModalWindow
        open={createGameWindowOpen}
        onClose={() => setCreateGameWindowOpen(false)}
      />
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
