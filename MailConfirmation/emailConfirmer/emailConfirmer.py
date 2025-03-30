import json
import logging

import flask
from flask import abort, jsonify, request
import sender
from db.db_session import create_session
from db.MailsConfimed import MailsConfimed

blueprint = flask.Blueprint(
    "email_confirmer",
    __name__,
)


logging.basicConfig(
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s', level=logging.DEBUG
)

logger = logging.getLogger(__name__)

@blueprint.route('/mail', methods=['POST'])
def mail():
    with create_session() as session:
        try:
            mail = MailsConfimed(request.json['email'], False)
            session.add(mail)
            sender.sender.send_email_smtp(request.json['email'], mail.confirmation_code)
            session.commit()
        except Exception as e:
            return {"status": "error", "message": str(e)}
        return {"status": "ok"}

@blueprint.route('/mail/confirm', methods=['PUT'])
def confirm():
    with create_session() as session:
        try:
            mail = session.query(MailsConfimed).filter(MailsConfimed.email == request.json['email']).first()
            if not mail:
                return {"status": "ok", "message": "Email not found"}
            if mail.confirmed:
                return {"status": "ok", "message": "Email already confirmed"}
            if mail.confirmation_code != request.json['confirmation_code']:
                return {"status": "ok", "message": "Confirmation code is not valid"}
            session.commit()
            return {"status": "ok", "message": "Email confirmed"}
        except Exception as e:
            return {"status": "error", "message": str(e)}
    