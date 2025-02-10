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

# Success
200 Ok

# User Exists
403 Forbidden

# Bad json
400 Bad Request
```

### Авторизация

```http
POST /login
Content-Type: application/json

{"username": "string", "password": "string"}

# Success
200 Ok

{"access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwZjgwYTZmOS0xNWVlLTQ3YWEtYmYzMC1lMzU4YmY1ZTE4ZjIiLCJuYW1lIjoidXNlcm5hbWUiLCJpYXQiOjE1MTYyMzkwMjJ9.Jkl_ogSDKvkPlfiiaW282sTqkgiZmvw4P0FH7026egI"}

# User does not exist or password is incorrect
404 Not Found

# Bad json
400 Bad Request
```
 https://jwt.io/#debugger-io?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwZjgwYTZmOS0xNWVlLTQ3YWEtYmYzMC1lMzU4YmY1ZTE4ZjIiLCJuYW1lIjoidXNlcm5hbWUiLCJpYXQiOjE1MTYyMzkwMjJ9.Jkl_ogSDKvkPlfiiaW282sTqkgiZmvw4P0FH7026egI

### Получение рейтинга текущего пользователя

``` http
GET /me
Authorization: token

# Success
200 Ok

{"username": "string", "rating": 0}

# Incorrect authorization
401 Unauthorized
```

### Список игр

``` http
# Получить список игр на странице 1 (отсчет с единицы), если считать что всего 20 элементов на каждой странице
GET /games?page=1&pagesize=20
Authorization: token

# Success
200 Ok

{
  "page": 1,
  "maxPage": 5,
  "content": [{"id": "guid", "status": "open|ongoing|completed", "createdAt": "datetime", "maxRating": 10, "creator": "string"}]
}

# Incorrect authorization
401 Unauthorized
```

У бэка нет возможности узнать, в каком состоянии изначально был список при запрашивании последующих страниц. Поэтому во время получения данных фронтом он должен проверить ID игр и удалить те, которые уже находятся на странице.

### Рейтинг

``` http
# топ-10 пользователей по рейтингу
GET /rating?limit=10
Authorization: token

# Success
200 Ok

[{"username": "string", "rating": 100}]

# Incorrect authorization
401 Unauthorized
```

### Создание игры

``` http
POST /games
Authorization: token
Content-Type: application/json

{"maxRating": 10}

# Success
201 Created
Location: /games/0f80a6f9-15ee-47aa-bf30-e358bf5e18f2

{"id": "0f80a6f9-15ee-47aa-bf30-e358bf5e18f2"}

# Incorrect authorization
401 Unauthorized

# Something wrong with json
400 Bad Request
```
