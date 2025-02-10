import classes from "./Login.module.css";
import InputField from "../../components/general/inputField/inputField.tsx";
import Button from "../../components/general/button/button.tsx";

function Login() {
  return (
    <div className={classes.container}>
      <h1>Tic Tac Toe</h1>
      <div className={classes.inputForm}>
        <InputField placeholder="Имя пользователя" />
        <InputField type="password" placeholder="Пароль" />
      </div>
      <Button style={{ marginTop: "36px" }}>Войти</Button>
    </div>
  );
}

export default Login;
