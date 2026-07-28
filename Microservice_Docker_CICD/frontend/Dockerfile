# ---------- Build stage ----------
FROM node:20-alpine AS build
WORKDIR /app

# Vite reads VITE_* env vars at BUILD time and bakes them into the bundle.
# Pass them as build args so the deployed app knows where the APIs live.
ARG VITE_AUTH_API_URL=http://localhost:5001
ARG VITE_PRODUCT_API_URL=http://localhost:5002
ENV VITE_AUTH_API_URL=$VITE_AUTH_API_URL
ENV VITE_PRODUCT_API_URL=$VITE_PRODUCT_API_URL

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
