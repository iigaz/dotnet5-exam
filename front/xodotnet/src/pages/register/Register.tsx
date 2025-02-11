import classes from "./Register.module.css";
import InputField from "../../components/general/inputField/inputField.tsx";
import Button from "../../components/general/button/button.tsx";
import { ChangeEvent, FormEvent, useState } from "react";
import api from "../../config/axios.ts";
import { useNavigate } from "react-router-dom";
import { AxiosError } from "axios";

function Register() {
  const navigator = useNavigate();

  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [passwordConfirmation, setPasswordConfirmation] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleSubmit = (
    username: string,
    password: string,
    passwordConfirmation: string,
  ) => {
    if (password !== passwordConfirmation) {
      setErrorMessage("Пароли должны совпадать");
      return;
    }

    api
      .post("/register", { username: username, password: password })
      .then((_) => {
        setErrorMessage(null);
        navigator("/login");
      })
      .catch((error: AxiosError<any, any>) => {
        if (!error.response) setErrorMessage("Что-то пошло не так.");
        else if (error.response.status === 403) {
          setErrorMessage("Такой пользователь уже существует");
        } else if (error.response.status === 400) {
          setErrorMessage("Неправильный формат данных");
        }
      });
  };

  return (
    <div className={classes.container}>
      <h1>Tic Tac Toe</h1>
      <form
        action="post"
        onSubmit={(e: FormEvent<HTMLFormElement>) => {
          e.preventDefault();
          handleSubmit(username, password, passwordConfirmation);
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
              onChange={(e: ChangeEvent<HTMLInputElement>) =>
                setPassword(e.target.value)
              }
              type="password"
              placeholder="Пароль"
            />
            <InputField
              onChange={(e: ChangeEvent<HTMLInputElement>) =>
                setPasswordConfirmation(e.target.value)
              }
              type="password"
              placeholder="Повторите пароль"
            />
            {errorMessage === null ? (
              <></>
            ) : (
              <div className={classes.errorMessage}>
                <p>{errorMessage}</p>
              </div>
            )}
          </div>
          <Button onClick={handleSubmit} style={{ marginTop: "36px" }}>
            Регистрация
          </Button>
        </div>
      </form>
    </div>
  );
}

export default Register;
