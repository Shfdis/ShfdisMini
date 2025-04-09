import json
import logging
import uuid
import flask
from flask import abort, jsonify, request
import sender.sender
from db.db_session import create_session
from db.MailsConfirmed import MailsConfirmed

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
            if session.query(MailsConfirmed).filter(MailsConfirmed.email == request.json['email']).first():
                return {"status": "error", "message": "Email already exists"}
            mail = MailsConfirmed(email=request.json['email'], confirmed=False, confirmation_code=str(uuid.uuid4()))
            session.add(mail)
            value = sender.sender.send_email_smtp(request.json['email'], mail.confirmation_code)
            session.commit()
            return {"status": "ok", "info": value}
        except Exception as e:
            logger.error(str(e))
            return {"status": "error", "message": "Could not send email"}, 
        

@blueprint.route('/mail/confirm', methods=['PUT'])
def confirm():
    with create_session() as session:
        try:
            mail = session.query(MailsConfirmed).filter(MailsConfirmed.email == request.json['email']).first()
            if not mail:
                return {"status": "ok", "message": "Email not found"}
            elif mail.confirmed:
                return {"status": "ok", "message": "Email already confirmed"}
            elif mail.confirmation_code != request.json['confirmation_code']:
                return {"status": "ok", "message": "Confirmation code is not valid"}
            else:
                mail.confirmed = True
                session.commit()
                return {"status": "ok", "message": "Email confirmed"}
        except Exception as e:
            return {"status": "error", "message": str(e)}
    