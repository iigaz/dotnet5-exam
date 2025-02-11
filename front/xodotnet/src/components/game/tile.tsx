import classes from "./tile.module.css";
import greenX from "../../assets/greenX.svg";
import redO from "../../assets/redO.svg";

function Tile(props: TileProps) {
  return (
    <div
      onClick={props.onTileClick}
      className={[
        props.bottomBorder ? classes.bottomBorder : null,
        props.rightBorder ? classes.rightBorder : null,
        classes.tile,
      ]
        .join(" ")
        .trim()}
    >
      {props.tileValue === "x" ? (
        <img src={greenX} />
      ) : props.tileValue === "o" ? (
        <img src={redO} />
      ) : (
        <></>
      )}
    </div>
  );
}

export default Tile;

export interface TileProps {
  onTileClick: () => void;
  tileValue: string;
  rightBorder: boolean;
  bottomBorder: boolean;
}
