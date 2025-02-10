import classes from "./ratingModalWindow.module.css";
import { Dialog, DialogContent, DialogTitle } from "@mui/material";

function RatingModalWindow(props: DialogProps) {
  return (
    <Dialog
      open={props.open}
      onClose={props.onClose}
      sx={{
        "& .MuiPaper-root": {
          backgroundColor: "var(--accent-color)",
        },
      }}
    >
      <DialogContent>
        <div className={classes.modalWindow}>
          <div>
            <h3>Рейтинг</h3>
          </div>
          {tempRatingList.map((userRating: UserRating, index) => (
            <div>
              <div>
                {index}
                {userRating.username}
              </div>
              <div>{userRating.rating}</div>
            </div>
          ))}
        </div>
      </DialogContent>
    </Dialog>
  );
}

export default RatingModalWindow;

export interface DialogProps {
  open: boolean;
  onClose: () => void;
}

export interface UserRating {
  username: string;
  rating: number;
}

const tempRatingList: UserRating[] = [
  { username: "user1", rating: 1004 },
  { username: "user2", rating: 103 },
  { username: "user3", rating: 102 },
  { username: "user4", rating: 101 },
  { username: "user5", rating: 100 },
  { username: "user6", rating: 99 },
  { username: "user7", rating: 99 },
];
