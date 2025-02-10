# XO.net

Игра Крестики-Нолики.

## Сущности

| User                 |
|----------------------|
| Id: Guid             |
| Username: string     |
| PasswordHash: string |
| Rating: int          |


| Game                      |
|---------------------------|
| Id: Guid                  |
| Creator: User             |
| CreatedDateTime: DateTime |
| Player1: User?            |
| Player2: User?            |

Состояния игры:

- `Player1 == null && Player2 == null` ⇒ Завершена (Completed)
- `Player1 == null || Player2 == null` ⇒ Открыта (Open) (a.k.a. Не начата)
- `Player1 != null && Player2 != null` ⇒ Начата (Ongoing)

## Эндпоинты

### Регистрация

```http
POST /register
Content-Type: application/json

{"username": "string", "password": "string"}

### Success
200 Ok

### User Exists
403 Forbidden

### Bad json
400 Bad Request
```

### Авторизация

```http
POST /login
Content-Type: application/json

{"username": "string", "password": "string"}

### Success
200 Ok

{"access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwZjgwYTZmOS0xNWVlLTQ3YWEtYmYzMC1lMzU4YmY1ZTE4ZjIiLCJuYW1lIjoidXNlcm5hbWUiLCJpYXQiOjE1MTYyMzkwMjJ9.Jkl_ogSDKvkPlfiiaW282sTqkgiZmvw4P0FH7026egI"}

### User does not exist or password is incorrect
404 Not Found

### Bad json
400 Bad Request
```
 https://jwt.io/#debugger-io?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwZjgwYTZmOS0xNWVlLTQ3YWEtYmYzMC1lMzU4YmY1ZTE4ZjIiLCJuYW1lIjoidXNlcm5hbWUiLCJpYXQiOjE1MTYyMzkwMjJ9.Jkl_ogSDKvkPlfiiaW282sTqkgiZmvw4P0FH7026egI

