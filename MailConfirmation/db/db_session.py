import sqlalchemy as sa
import sqlalchemy.orm as orm
from sqlalchemy.orm import Session
import sqlalchemy.ext.declarative as dec

SqlAlchemyBase = dec.declarative_base()

__factory = None


def global_init(db_user, db_password, db_host, db_name):
    global __factory

    if __factory:
        return

    if not db_name or not db_name.strip():
        raise Exception("Необходимо указать имя базы данных.")

    conn_str = f'postgresql://{db_user}:{db_password}@{db_host}/{db_name}'
    print(f"Подключение к базе данных по адресу {conn_str}")

    engine = sa.create_engine(conn_str, echo=True)
    __factory = orm.sessionmaker(bind=engine)

    from emailConfirmer import emailConfirmer

    SqlAlchemyBase.metadata.create_all(engine)


def create_session() -> Session:
    global __factory
    return __factory()