docker kill webapp

docker build -t webapp .

docker run --env CONNECTION="Server=host.docker.internal;user=root;Database=NordicDoor;port=3306;Password=Testingtesting1234;" --publish 80:80 webapp
