import classes from "./MainPage.module.css";
import exitIcon from "../../assets/exitIcon.svg";
import Button from "../../components/general/button/button.tsx";
import GamesList, {
  GamesListProps,
} from "../../components/gamesList/gamesList.tsx";
import RatingModalWindow from "../../components/mainPage/mainPageModalWindows/ratingModalWindow/ratingModalWindow.tsx";
import { useEffect, useState } from "react";
import CreateNewGameModalWindow from "../../components/mainPage/mainPageModalWindows/createNewGameModalWindow/createNewGameModalWindow.tsx";
import UserInfo, {
  UserInfoProps,
} from "../../components/mainPage/userInfo/userInfo.tsx";
import api from "../../config/axios.ts";
import { useNavigate } from "react-router-dom";
import { AxiosError } from "axios";
import CustomLoader from "../../components/general/loader/customLoader.tsx";

function MainPage() {
  const navigator = useNavigate();

  const [userInfo, setUserInfo] = useState<UserInfoProps | null>(null);
  const [gamesFirstPage, setGamesFirstPage] = useState<GamesListProps | null>(
    null,
  );
  const [ratingWindowOpen, setRatingWindowOpen] = useState(false);
  const [createGameWindowOpen, setCreateGameWindowOpen] = useState(false);

  const handleExit = () => {
    localStorage.removeItem("access_token");
    delete api.defaults.headers.common["Authorization"];
    navigator("/auth");
  };

  useEffect(() => {
    api
      .get("/me")
      .then((response) => setUserInfo(response.data))
      .catch((error: AxiosError<any, any>) => {
        if (!error.response) {
          setUserInfo(null);
        } else if (error.response.status === 401) {
          navigator("/auth");
        }
      });
  }, []);

  useEffect(() => {
    api
      .get("/games?page=1&pagesize=20")
      .then((response) => setGamesFirstPage(response.data))
      .catch((error: AxiosError<any, any>) => {
        if (!error.response) {
          setGamesFirstPage(tempList);
        } else if (error.response.status === 401) {
          navigator("/auth");
        }
      });
  }, []);

  return (
    <>
      <div className={classes.container}>
        <div className={classes.gameAndRating}>
          <h1>Tic Tac Toe</h1>
          <Button onClick={() => setRatingWindowOpen(true)}>Рейтинг</Button>
          <Button onClick={() => setCreateGameWindowOpen(true)}>
            Создать игру
          </Button>
          <div className={classes.exitButtonContainer}>
            <Button onClick={handleExit}>
              <div className={classes.exitButton}>
                <span>Выйти</span>
                <img src={exitIcon} className={classes.exitIcon} />
              </div>
            </Button>
          </div>
        </div>
        <UserInfo {...userInfo} />
        {gamesFirstPage === null ? (
          <div className={classes.listWrapperSkeleton}>
            <CustomLoader />
          </div>
        ) : (
          <GamesList {...gamesFirstPage} />
        )}
      </div>
      <RatingModalWindow
        open={ratingWindowOpen}
        onClose={() => setRatingWindowOpen(false)}
      />
      <CreateNewGameModalWindow
        open={createGameWindowOpen}
        onClose={() => setCreateGameWindowOpen(false)}
      />
    </>
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
