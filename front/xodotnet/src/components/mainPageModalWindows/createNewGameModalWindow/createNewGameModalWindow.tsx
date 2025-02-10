import { Dialog, DialogContent, DialogTitle } from "@mui/material";
import { DialogProps } from "../ratingModalWindow/ratingModalWindow.tsx";

function CreateNewGameModalWindow(props: DialogProps) {
  return (
    <Dialog open={props.open} onClose={props.onClose}>
      <DialogTitle></DialogTitle>
      <DialogContent></DialogContent>
    </Dialog>
  );
}

export default CreateNewGameModalWindow;
