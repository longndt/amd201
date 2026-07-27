# ---------- Build stage ----------
FROM node:20-alpine AS build
WORKDIR /app

# Vite reads VITE_* env vars at BUILD time and bakes them into the bundle.
# The frontend talks only to the API Gateway, so it needs a single URL.
ARG VITE_API_URL=http://localhost:7000
ENV VITE_API_URL=$VITE_API_URL

COPY package*.json ./
RUN npm install

COPY . ./
RUN npm run build

# ---------- Runtime stage (static files served by nginx) ----------
FROM nginx:alpine AS runtime
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
