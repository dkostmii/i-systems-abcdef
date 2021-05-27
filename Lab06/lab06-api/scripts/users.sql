CREATE USER psen WITH PASSWORD 'qwerty1234';

GRANT PSEN_PROJECT TO psen;

CREATE USER readonly_user WITH PASSWORD 'qwerty1234';

GRANT psen_access TO readonly_user;

CREATE USER modify_user WITH PASSWORD 'qwerty1234';

GRANT psen_modify TO modify_user;
