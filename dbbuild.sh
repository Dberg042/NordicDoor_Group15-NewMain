docker kill is201-mariadb

docker run --rm --name is201-mariadb -p 127.0.0.1:3306:3306/tcp -v "$(pwd)/database":/var/lib/mysql -e MYSQL_ROOT_PASSWORD=Testingtesting1234 -d mariadb:latest
