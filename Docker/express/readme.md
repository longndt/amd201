## 1. Build Docker Image

```bash
docker build -t docker-express .
```

## 2. Run Docker Image

```bash
docker run -p 3000:3000 docker-express
```

## 3. Log in to Docker Hub

```bash
docker login -u longndt
```

## 4. Tag Docker Image

```bash
docker tag docker-express longndt/docker-express:latest
```

## 5. Push Docker Image

```bash
docker push longndt/docker-express:latest
```

## Notes

- `3000` is the server port.
- `docker-express` is the Docker image name.
- `longndt` is the Docker Hub username.
