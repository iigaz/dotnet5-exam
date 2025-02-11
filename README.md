# XO.net

Игра Крестики-Нолики.

## Запуск

В корне репозитория:

``` shell
$ docker compose build
$ docker compose up
```

- Бэкенд находится по адресу http://localhost:5000/swagger
- Фронтенд находится по адресу http://localhost:5173/

Внимание! Дальнейшая документация создавалась для внутренних нужд.

## Сущности

| User                 |
|----------------------|
| Id: Guid             |
| Username: string     |
| PasswordHash: string |

Отдельно в Monga: `Rating: int`


| Game                      |
|---------------------------|
| Id: Guid                  |
| Creator: User             |
| CreatedDateTime: DateTime |
| Status: Enum              |

Отдельно в MongoDB:

| Game           |
|----------------|
| Player1: User? |
| Player2: User? |
| Field: string  |
| Turn: int      |

Player1 - всегда крестик. Player2 - всегда нолик. При создании игры Turn == 1 когда ходят крестики, 2 когда ходят нолики, 0 если никто не может ходить.

Field хранится как строка вида "-xo-o-x--", где `-` - пустое место, `x` - крестик, `o` - нолик, каждые три символа - соответствующая строка 2d поля.

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

### Просмотр игры

``` http
GET /games/0f80a6f9-15ee-47aa-bf30-e358bf5e18f2
Authorization: application/json

# Success
200 Ok

{
  "player1": {"username": "string", "rating": 0} | null,
  "player2": {"username": "string", "rating": 0} | null,
  "field": "-xo-o-x--",
  "turn": 2,
}

# Incorrect authorization
401 Unauthorized
```

Сама игра выполняется через signalR.

### Ход игры (signalr)

Эндпоинт хаба: `/games/hub`

```
# Получение нового состояния игры от сервера
< updateState(field, turn, player1, player2)

# Получения объявления победителем
< declareWinner(winner, player1, player2)
# winner имеет такие же значения как и turn, но 0 - ничья
# после победы таймер 3 секунды на бэкенде, и после этого он посылает updateState с новой игрой

## Комманды бэка (бэкенд в ответ всем наблюдателям пришлет update_state если команда была завершена успешно)

> placeMark(gameId, x, y)
# Поставить марку (крестик или нолик решит бэк) в координатах x, y (оба с нуля; x - номер столбца; y - номер строки)

> spectate(gameId)
# Подписаться на обновления состояния игры

> join(gameId)
# Присоедитится

> leave(gameId)
# Отписаться от обновлений состояния и покинуть игру (если в игре)
```

## Архитектура бэкенда

Чистая архитектура + CQRS:

- `XoDotNet.Main` - слой представления (контроллеры и хаб)
- `XoDotNet.Features` - слой бизнес-логики (здесь используется CQRS)
- `XoDotNet.Domain` - слой доменных сущностей
- `XoDotNet.Infrastructure` - слой инфраструктуры с самописным медиатором
- `XoDotNet.DataAccess` - слой репозиториев и работы с базами данных

Также в сборке:
- `XoDotNet.Mediator` - отдельный проект с реализацией медиатора
- `XoDotNet.GameEvents` - проект с событиями RabbitMQ и их потребителями

