Запросы:
1. GET / - проверить, запущен ли сервис(возвращает "success")
2. POST /signup - зарегистрироваться 
принимает объект:
{
    "email": <email>,
    "password": <password>
}

возврает:
{
    "status": "success" | "error",
    "message": <сообщение об ошибке>
}
3. DELETE /delete - удалить пользователя
принимает объект:
{
    "email": <email>
    "password": <password>
}

возврает:
{
    "status": "success" | "error",
    "message": <сообщение об ошибке>
}
4. POST /session - войти

принимает объект:
{
    "email": <email>,
    "password": <password>
}

возврает:
{
    "status": "success" | "error",
    "token": <токен для авторизации>,
    "message": <сообщение об ошибке>
}

5. GET /session/<token> - проверить авторизацию

возвращает:
{
    "status": "success" | <сообщение об ошибке>
}

6. DELETE /session/<token> - выйти

возвращает:
{
    "status": "success" | <сообщение об ошибке>
}   