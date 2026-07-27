# Deployment env files (Render)

The images are built and pushed to **Docker Hub** by CI; Render **pulls** those images.
These files list the environment variables each Render service needs.

| File | Paste into |
|------|-----------|
| [`render.gateway.env.example`](render.gateway.env.example) | Gateway web service (Environment → Add from .env) |
| [`render.authservice.env.example`](render.authservice.env.example) | Auth web service |
| [`render.productservice.env.example`](render.productservice.env.example) | Product web service |
| [`render.frontend.env.example`](render.frontend.env.example) | **GitHub** repo variable (build-time, not Render) |

## Golden rules

1. **`Jwt__Key`, `Jwt__Issuer`, `Jwt__Audience` must be identical** on the Auth and
   Product services. Different values → **401 Unauthorized** on every product call.
2. **The gateway** must know your deployed service hostnames — set the
   `Routes__N__DownstreamHostAndPorts__0__Host` vars (or edit `ocelot.Production.json`).
3. **CORS lives on the gateway only** (`AllowedOrigins` = your frontend URL). The Auth
   and Product services are not browser-facing anymore.
4. **The frontend's gateway URL is baked at build time** via the GitHub `VITE_API_URL`
   variable — change it, re-run CI, redeploy the frontend.
5. **Never commit real secrets.** These `.example` files hold placeholders only.

## Deployment order

1. Create the 4 Docker Hub repos (public): `lab-gateway`, `lab-authservice`,
   `lab-productservice`, `lab-frontend`.
2. Push to `main` → CI builds & pushes all 4 images to Docker Hub.
3. On Render, create image-based **Web Services** for gateway / auth / product and paste
   the env vars above. Note each service's URL.
4. Set the gateway's `Routes__*` and `AllowedOrigins`; set `VITE_API_URL` (GitHub) to the
   gateway URL and re-run CI so the frontend image points at the gateway.
5. Create the frontend image service. Done.

See [`../README.md`](../README.md) → **Part H & Part I** for the full walkthrough.
