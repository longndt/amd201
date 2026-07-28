## 1. Build Docker Image

```bash
docker build -t docker-mvc .
```

## 2. Run Docker Image

```bash
docker run -p 8080:8080 docker-mvc
```

## 3. Log in to Docker Hub

```bash
docker login -u longndt
```

## 4. Tag Docker Image

```bash
docker tag docker-mvc longndt/docker-mvc:latest
```

## 5. Push Docker Image

```bash
docker push longndt/docker-mvc:latest
```

## Notes

- `8080` is the web server port.
- `docker-mvc` is the Docker image name.
- `longndt` is the Docker Hub username.
