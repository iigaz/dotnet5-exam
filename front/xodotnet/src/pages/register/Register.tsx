import classes from "./Register.module.css";
import InputField from "../../components/general/inputField/inputField.tsx";
import Button from "../../components/general/button/button.tsx";

function Register() {
  return (
    <div className={classes.container}>
      <h1>Tic Tac Toe</h1>
      <div className={classes.inputForm}>
        <InputField placeholder="Имя пользователя" />
        <InputField type="password" placeholder="Пароль" />
        <InputField type="password" placeholder="Повторите пароль" />
      </div>
      <Button style={{ marginTop: "36px" }}>Войти</Button>
    </div>
  );
}

export default Register;
