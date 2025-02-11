import { Dialog, DialogContent } from "@mui/material";
import { DialogProps } from "../ratingModalWindow/ratingModalWindow.tsx";
import classes from "../createNewGameModalWindow/createNewGameModalWindow.module.css";
import closeButton from "../../../assets/closeButton.svg";
import InputField from "../../general/inputField/inputField.tsx";
import Button from "../../general/button/button.tsx";

function CreateNewGameModalWindow(props: DialogProps) {
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
          <InputField placeholder="Макс. кол-во рейтинга" />
          <Button>Создать</Button>
        </div>
      </DialogContent>
    </Dialog>
  );
}

export default CreateNewGameModalWindow;
