import logging

from flask import Flask
from flask_cors import CORS
from flask_login import LoginManager
from flask_restful import Api
import os
import db.db_session
from emailConfirmer.emailConfirmer import blueprint as emailConfirmer

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
    db.db_session.global_init(db_user=os.environ["DB_USER"], db_password=os.environ["DB_PASSWORD"], db_host=os.environ["DB_HOST"], db_name='shfdismini')
    app.register_blueprint(emailConfirmer)



deploy()
if __name__ == '__main__':
    app.run()