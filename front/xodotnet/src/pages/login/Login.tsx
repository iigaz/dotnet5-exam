import classes from "./Login.module.css";
import InputField from "../../components/general/inputField/inputField.tsx";
import Button from "../../components/general/button/button.tsx";
import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../config/axios.ts";
import { AxiosError } from "axios";
import checkLoggedIn from "../../helpers/checkLoggedIn.ts";

function Login() {
  const navigator = useNavigate();

  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleSubmit = (username: string, password: string) => {
    api
      .post("/login", { username: username, password: password })
      .then((response) => {
        setErrorMessage(null);
        localStorage.setItem("access_token", response.data.access_token);
        navigator("/games");
      })
      .catch((error: AxiosError<any, any>) => {
        if (!error.response) setErrorMessage("Что-то пошло не так.");
        else if (error.response.status === 404) {
          setErrorMessage("Неверные логин или пароль");
        } else if (error.response.status === 400) {
          setErrorMessage("Неправильный формат данных");
        }
      });
  };

  useEffect(() => {
    if (checkLoggedIn()) navigator("/games");
  }, []);

  return (
    <div className={classes.container}>
      <h1>Tic Tac Toe</h1>
      <form
        action="post"
        onSubmit={(e: FormEvent<HTMLFormElement>) => {
          e.preventDefault();
          handleSubmit(username, password);
        }}
      >
        <div className={classes.formContainer}>
          <div className={classes.inputForm}>
            <InputField
              onChange={(e: ChangeEvent<HTMLInputElement>) =>
                setUsername(e.target.value)
              }
              placeholder="Имя пользователя"
            />
            <InputField
              type="password"
              onChange={(e: ChangeEvent<HTMLInputElement>) =>
                setPassword(e.target.value)
              }
              placeholder="Пароль"
            />
            {errorMessage === null ? (
              <></>
            ) : (
              <div className={classes.errorMessage}>
                <p>{errorMessage}</p>
              </div>
            )}
          </div>
          <Button style={{ marginTop: "36px" }}>Войти</Button>
        </div>
      </form>
    </div>
  );
}

export default Login;
