import datetime
import json
import sqlalchemy
from db.db_session import SqlAlchemyBase


class MailsConfimed(SqlAlchemyBase):
    __tablename__ = 'mails_confimed'
    email = sqlalchemy.Column(sqlalchemy.String,
                           primary_key=True)
    confirmed = sqlalchemy.Column(sqlalchemy.Boolean)
    confirmation_code = sqlalchemy.Column(sqlalchemy.String)