В каждом проекте есть свой README.md файл, который описывает все запросы к API и их ответы.
для запуска:
```bash
docker compose up --build
```

Таблицы в БД

password_hashings
email(PRIMARY KEY): STR|password_hashing: STR

mail_cofirmations
email(PRIMARY KEY): STR|condirmed: BOOL|confirmation_code: STR

Session:
id(PRIMARY KEY): INT|user_id: STR|session_token: STR|date: DATETIME
