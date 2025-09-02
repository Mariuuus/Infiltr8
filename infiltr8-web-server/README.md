```
cd server
dotnet restore
dotnet publish
cd ..
sudo docker build -f docker/Dockerfile  . -t server
sudo docker compose -f docker/docker-compose.yml up


sudo docker build -f docker/Dockerfile -t gitlab.hs-anhalt.de:5050/sprog25/infiltr8 ./
```


# Routes

`api/user`
    - `GET` -> 200, wenn username + password richtig
    - `POST` -> 201, wenn username + password zum erstellen angebeben, 409, wenn username in beutzung