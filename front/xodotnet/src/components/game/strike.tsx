import classes from "./strike.module.css";

function Strike(props: StrikeProps) {
  return (
    <div className={[classes.strike, props.strikeClass].join(" ").trim()}></div>
  );
}

export default Strike;

export interface StrikeProps {
  strikeClass: string;
}
