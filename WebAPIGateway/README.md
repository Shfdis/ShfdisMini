Проверка работы сервера
GET /
Проверка доступности сервиса
Ответ: {"status": "ok"}

Работа с почтой
POST /mail
Отправка письма с подтверждением
Проксирует в: MAIL_HOST/mail

PUT /mail/confirm
Подтверждение email по коду
Проксирует в: MAIL_HOST/mail/confirm

Регистрация и авторизация
POST /signup
Регистрация нового пользователя
Проксирует в: LOGIN_HOST/signup

DELETE /user/delete
Удаление пользователя
Проксирует в: LOGIN_HOST/delete

Управление сессиями
POST /session
Создание новой сессии (логин)
Проксирует в: LOGIN_HOST/session

GET /session/:token
Проверка сессии по токену
Проксирует в: LOGIN_HOST/session/:token

DELETE /session/:token
Завершение сессии (выход)
Проксирует в: LOGIN_HOST/session/:token