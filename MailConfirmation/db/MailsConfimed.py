import datetime
import json
import uuid
import sqlalchemy
from db.db_session import SqlAlchemyBase


class MailsConfimed(SqlAlchemyBase):
    def __init__(self, email, confirmed):
        self.email = email
        self.confirmed = confirmed
        confirmation_code = uuid.uuid4()
    __tablename__ = 'mails_confimed'
    email = sqlalchemy.Column(sqlalchemy.String,
                           primary_key=True)
    confirmed = sqlalchemy.Column(sqlalchemy.Boolean)
    confirmation_code = sqlalchemy.Column(sqlalchemy.String)