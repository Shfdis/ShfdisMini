import logging

from flask import Flask
from flask_cors import CORS
from flask_login import LoginManager
from flask_restful import Api

import db.db_session
import emailConfirmer.emailConfirmer

app = Flask(__name__)
CORS(app)

api = Api(app)
login_manager = LoginManager()
login_manager.init_app(app)

logging.basicConfig(
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s', level=logging.DEBUG
)

logger = logging.getLogger(__name__)


@app.route('/')
def index_map():
    return "ok"


def deploy():
    db.db_session.global_init(db_user='postgresql', db_password='postgresql', db_host='db',
                                       db_port='5432', db_name='shfdismini')
    app.register_blueprint(emailConfirmer)


deploy()
if __name__ == '__main__':
    app.run(debug=True)