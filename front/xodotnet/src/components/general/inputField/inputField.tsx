import classes from "./inputField.module.css";

function InputField({ ...props }) {
  return (
    <input
      type="text"
      {...props}
      className={[props.className, classes.inputField].join(" ").trim()}
    ></input>
  );
}

export default InputField;
