import { Dialog, DialogContent } from "@mui/material";
import { DialogProps } from "../ratingModalWindow/ratingModalWindow.tsx";
import classes from "../createNewGameModalWindow/createNewGameModalWindow.module.css";
import closeButton from "../../../../assets/closeButton.svg";
import InputField from "../../../general/inputField/inputField.tsx";
import Button from "../../../general/button/button.tsx";
import { useState } from "react";
import api from "../../../../config/axios.ts";
import { useNavigate } from "react-router-dom";
import { AxiosError } from "axios";

function CreateNewGameModalWindow(props: DialogProps) {
  const navigator = useNavigate();

  const [maxRating, setMaxRating] = useState<number>(2_147_483_647);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleSubmit = (maxRating: number) => {
    api
      .post("/games", { maxRating: maxRating })
      .then((response) => {
        setErrorMessage(null);
        navigator(`/games/${response.data.id}`);
      })
      .catch((error: AxiosError<any, any>) => {
        if (!error.response) {
          setErrorMessage("Ошибка. Пожалуйста попробуйте снова");
        } else if (error.response.status === 401) {
          navigator("/auth");
        } else if (error.response.status === 400) {
          setErrorMessage("Неверный формат данных");
        }
      });
  };

  return (
    <Dialog
      open={props.open}
      onClose={props.onClose}
      sx={{
        "& .MuiPaper-root": {
          backgroundColor: "var(--accent-color)",
        },
      }}
      fullWidth
    >
      <DialogContent>
        <div className={classes.closeButton} onClick={props.onClose}>
          <img src={closeButton} />
        </div>
        <div className={classes.modalWindow}>
          <div>
            <h3>Создать игру</h3>
          </div>
          <form
            action="post"
            onSubmit={(e: React.FormEvent<HTMLFormElement>) => {
              e.preventDefault();
              handleSubmit(maxRating);
            }}
          >
            <div className={classes.inputFormContainer}>
              <InputField
                onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                  setMaxRating(Number(e.target.value))
                }
                placeholder="Макс. кол-во рейтинга"
              />
              {errorMessage === null ? (
                <></>
              ) : (
                <div className={classes.errorMessage}>
                  <p>{errorMessage}</p>
                </div>
              )}
              <Button>Создать</Button>
            </div>
          </form>
        </div>
      </DialogContent>
    </Dialog>
  );
}

export default CreateNewGameModalWindow;
