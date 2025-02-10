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


