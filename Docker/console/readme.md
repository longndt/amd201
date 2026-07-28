## 1. Build Docker Image

```bash
docker build -t docker-console .
```

## 2. Run Docker Image

```bash
docker run docker-console
```

## 3. Log in to Docker Hub

```bash
docker login -u longndt
```

## 4. Tag Docker Image

```bash
docker tag docker-console longndt/docker-console:latest
```

## 5. Push Docker Image

```bash
docker push longndt/docker-console:latest
```

## Note

- `docker-console` is the Docker image name.
- `longndt` is the Docker Hub username.
