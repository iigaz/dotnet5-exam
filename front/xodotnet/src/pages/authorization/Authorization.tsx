import Button from "../../components/general/button/button.tsx";
import { Link } from "react-router-dom";
import classes from "./Autorization.module.css";

function Authorization() {
  return (
    <div className={classes.container}>
      <h1>Tic Tac Toe</h1>
      <Link to="/login">
        <Button>Войти</Button>
      </Link>
      <p>ИЛИ</p>
      <Link to="/register">
        <Button>Регистрация</Button>
      </Link>
    </div>
  );
}

export default Authorization;
