# Deployment env files (Render)

Copy-paste-ready environment variables for the three Render services. In the Render
dashboard, open a service → **Environment** → **Add from .env** → paste the matching
file's contents, then replace every `<PLACEHOLDER>`.

| File | Paste into |
|------|-----------|
| [`render.authservice.env.example`](render.authservice.env.example) | Auth web service |
| [`render.productservice.env.example`](render.productservice.env.example) | Product web service |
| [`render.frontend.env.example`](render.frontend.env.example) | Frontend static site |

## Golden rules

1. **`Jwt__Key`, `Jwt__Issuer`, `Jwt__Audience` must be identical** on the Auth and
   Product services. Different values → **401 Unauthorized** on every product call.
2. **`AllowedOrigins`** on both backends must equal your **frontend URL** exactly
   (scheme + host, no trailing slash) or the browser blocks the calls (CORS).
3. **`VITE_*`** vars are baked at build time — **redeploy** the frontend after changing them.
4. **Never commit real secrets.** These `.example` files contain placeholders only; the
   real values live in the Render dashboard.

## Deployment order

1. Deploy **Auth** and **Product** services → note their URLs.
2. Deploy the **frontend** with `VITE_AUTH_API_URL` / `VITE_PRODUCT_API_URL` set to those URLs.
3. Set `AllowedOrigins` on both backends to the frontend URL → redeploy the backends.

See [`../README.md`](../README.md) → **Part H** for the full walkthrough.
