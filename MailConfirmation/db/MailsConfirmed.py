import datetime
import json
import sqlalchemy
from db.db_session import SqlAlchemyBase


class MailsConfirmed(SqlAlchemyBase):
    __tablename__ = 'mails_confirmed'
    email = sqlalchemy.Column(sqlalchemy.String,
                           primary_key=True)
    confirmed = sqlalchemy.Column(sqlalchemy.Boolean)
    confirmation_code = sqlalchemy.Column(sqlalchemy.String)