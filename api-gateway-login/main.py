from flask import Flask
from flask_login import LoginManager
from flask_restful import Api
app = Flask(__name__)
api = Api(app)
login_manager = LoginManager()
login_manager.init_app(app)
@app.route('/')
def check():
    return "{'status':'ok'}"

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port='8080')