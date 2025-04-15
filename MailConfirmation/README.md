запросы:
1. Отправка кода подтверждения
POST /mail

Описание: Отправляет код подтверждения на email.

Тело запроса (JSON):

{ "email": "user@example.com" }
Ответы:

Успех: {"status": "ok", "info": "..."}

Ошибка: {"status": "error", "message": "..."}

2. Подтверждение email
PUT /mail/confirm

Описание: Проверяет код и подтверждает email.

Тело запроса (JSON):

{
  "email": "user@example.com",
  "confirmation_code": "код_подтверждения"
}
Ответы:

Успех: {"status": "ok", "message": "Email confirmed"}

Ошибка: {"status": "error", "message": "..."}