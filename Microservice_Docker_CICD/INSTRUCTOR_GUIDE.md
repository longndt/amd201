# Instructor Guide

Companion to [`README.md`](README.md) (the student lab). This guide helps you run the
session: timing, talking points, checkpoints, the mistakes students actually make, a
grading rubric, and discussion questions.

---

## 1. At a glance

| | |
|---|---|
| **Audience** | Students with basic C#, JavaScript, and HTTP knowledge |
| **Prereqs to verify beforehand** | .NET 8 SDK, Node 20+, Docker Desktop, Git, GitHub + Docker Hub accounts |
| **Delivery** | Guided walkthrough + independent build, or flipped (students read README first) |
| **Total time** | ~4–5 hours (best split across 2 sessions) |
| **Deliverable** | Public GitHub repo + images on Docker Hub + live Render URLs + short demo |

### Port map (memorize this — it comes up constantly)

| Component | Port |
|-----------|------|
| API Gateway (Ocelot) — the only browser-facing API | **7000** |
| Auth service | **7001** |
| Product service | **7002** |
| Frontend nginx (Docker) | **7080** |
| Frontend Vite dev server | **7173** |
| SQL Server (host mapping) | **14330** (container stays 1433) |

All ports are deliberately non-default to avoid clashes with whatever students already
have running (IIS Express, other SQL Servers, other labs).

### Recommended split across two sessions
- **Session 1 (2–2.5h):** Parts A–F — the two services + gateway + React running locally.
- **Session 2 (2h):** Parts G–I — Docker Compose, CI/CD → Docker Hub, Render deploy.

---

## 2. Suggested timing

| Segment | Time | Notes |
|--------|------|-------|
| Intro + architecture (slides) | 20 min | Use `docs/slides.html`. Emphasize gateway-as-single-entry-point + JWT-across-services. |
| Part A — SQL Server in Docker | 10 min | Everyone runs the same `docker run` (port **14330**). Confirm `docker ps`. |
| Part B — Auth service | 30 min | Walk the code, then let them run (:7001) + register in Swagger. |
| Part C — Product service | 30 min | Focus on `[Authorize]` and JWT validation config (:7002). |
| **Checkpoint 1–2** | 5 min | Everyone has a token; 401 → Authorize → 200 on `GET /products`. |
| Part D — Ocelot gateway | 30 min | New concept for most students — go slow on the routing JSON. |
| **Checkpoint 3** | 5 min | Login + products both work **through :7000**. |
| Part E — React frontend | 35 min | Single `VITE_API_URL`, AuthContext, protected routes (:7173). |
| Part F — full local run | 10 min | Register → login → CRUD end-to-end via the gateway. |
| **Checkpoint 4** | 5 min | Working app in the browser. |
| Part G — Docker Compose | 25 min | One command; explain `sqlserver` hostname + `ocelot.Docker.json`. |
| Part H — CI/CD → Docker Hub | 30 min | Docker Hub repos, secrets, watch Actions push 4 images. |
| Part I — Render deploy from images | 40 min | The slowest part; deploy order matters (services → gateway → frontend). |
| Wrap-up + Q&A | 15 min | Discussion questions below. |

---

## 3. Talking points per part (the "why")

**Architecture.** Ask: *"Why two services instead of one?"* Draw out independent
deploy/scaling and separate databases. Then: *"Why a gateway?"* — one URL for the
frontend, one place for CORS/rate-limiting/logging, and internal services can stay
private. Show `docs/images/architecture.svg`.

**Auth service.** The big idea is **we never store raw passwords**. Open
`Services/PasswordHasher.cs` and explain salt + iterations. Then explain the JWT: it's a
signed statement "this is user X", not encryption — anyone can read the payload, but
nobody can forge it without the key. Show https://jwt.io by pasting a token.

**Product service.** The `[Authorize]` attribute + `AddJwtBearer` config. Stress the
**shared key** rule (`docs/images/jwt-flow.svg`). Demo the 401 first, then authorize,
then 200 — the "aha" moment.

**Gateway (Ocelot).** Three teaching points: (1) it's *just routing* — a small ASP.NET
app with a JSON routing table; (2) it **forwards** the `Authorization` header but does
NOT validate the JWT — the Product service still does that (defense stays at the
service); (3) there are **three routing files** for three environments
(`ocelot.json` local, `ocelot.Docker.json` compose, `ocelot.Production.json` Render) —
selected by `ASPNETCORE_ENVIRONMENT`. This "same image, different config" idea is
exactly how real deployments work.

**React.** Three teaching points: (1) env vars are **build-time** in Vite; (2) the app
knows only ONE URL — the gateway (`VITE_API_URL`); (3) `ProtectedRoute` gates the UI,
but **the real security is server-side** — the UI guard is just UX.

**Docker.** Multi-stage builds (build image vs runtime image) and *why* images stay
small. The `sqlserver` vs `localhost` hostname difference inside the compose network is
the #1 confusion — call it out explicitly.

**CI/CD → Docker Hub.** The proper flow: *"the pipeline is the only thing that builds
images; Docker Hub is where they live; Render just pulls them."* Benefits to draw out:
the exact image you tested is the one deployed, images are versioned by commit SHA, and
any host (not just Render) could pull and run them. Map the jobs to
`docs/images/cicd-flow.svg`.

**Render.** Deploy from **existing images** (not from source). Config comes from
**environment variables** — including the gateway's `Routes__*` overrides, which
repoint the routing table without rebuilding the image. Deploy order matters:
services → gateway (needs service URLs) → frontend (needs gateway URL baked in).

---

## 4. Checkpoints (verify before moving on)

1. **After Part A:** `docker ps` shows `lab-sqlserver` (port 14330).
2. **After Part C:** In Product Swagger (:7002), `GET /api/products` returns **401**
   without a token and **200** after Authorize. *(Proves cross-service JWT works.)*
3. **After Part D:** `POST http://localhost:7000/api/auth/login` returns a token and
   `GET http://localhost:7000/api/products` (with Bearer) returns products.
   *(Proves the gateway routes correctly.)*
4. **After Part F:** Full register → login → create/edit/delete in the browser (:7173).
5. **After Part G:** `docker compose up --build` serves the app at `localhost:7080`.
6. **After Part H:** Four images visible on Docker Hub with `:latest` + SHA tags.
7. **After Part I:** Public Render frontend URL works end-to-end.

---

## 5. Common student mistakes (and the fix)

| Mistake | Symptom | Fix |
|--------|---------|-----|
| JWT settings differ between services | Product API always 401 | Copy the **exact** `Jwt__Key/Issuer/Audience` to both |
| Using `localhost` for DB inside compose | Service can't reach DB in Docker | Use host `sqlserver,1433` in compose connection strings |
| Part A container left running before compose | `docker compose up` fails: name/port conflict | `docker rm -f lab-sqlserver` first (called out in README) |
| Wrong SQL port when running locally | Connection refused | Local `dotnet run` uses `localhost,14330` (host mapping) |
| Calling services directly from React | CORS errors / hardcoded URLs | The frontend must call **only the gateway** (`VITE_API_URL`) |
| Gateway 404 on every route | Wrong ocelot file loaded | Check `ASPNETCORE_ENVIRONMENT` (Development/Docker/Production) |
| Gateway 502 in compose | Downstream host wrong | `ocelot.Docker.json` must use `authservice`/`productservice:8080` |
| Forgetting CORS after deploy | Browser blocks calls on Render | Set `AllowedOrigins` **on the gateway** to the frontend URL |
| Editing `.env` and expecting live change | Frontend still hits old URL | Vite bakes env at build; restart dev / rebuild image |
| CI push fails | `docker-push` job errors | Add `DOCKERHUB_USERNAME` + `DOCKERHUB_TOKEN` secrets; create the 4 repos |
| Render can't pull the image | Deploy fails immediately | Image URL typo, or private repo without credentials |
| Weak SA password | SQL Server container won't start | 8+ chars incl. upper, lower, digit |
| Pasting `Bearer ` prefix in Swagger Authorize | Still 401 | Paste the **raw** token only |

---

## 6. Grading rubric (100 pts)

| Criterion | Pts | What to look for |
|----------|-----|------------------|
| Auth service works | 12 | Register + login return a JWT; passwords hashed |
| Product CRUD works | 12 | All four operations; correct status codes |
| Auth enforced across services | 12 | 401 without token, 200 with valid token |
| **API Gateway routes correctly** | 12 | All traffic via :7000; correct ocelot config per environment |
| React frontend | 12 | Login/register + CRUD UI; calls gateway only; protected route |
| Dockerization | 10 | Valid Dockerfiles; `docker compose up` runs the stack on :7080 |
| **CI/CD pushes to Docker Hub** | 10 | Actions builds, tests, pushes 4 tagged images |
| Render deployment from images | 10 | Live app; gateway env-var routing configured |
| Code quality & structure | 5 | DTOs, separation, no secrets committed |
| Documentation / demo | 5 | README notes, screenshots, or short walkthrough |

**Bonus (up to +10):** rate limiting or caching on the gateway, refresh tokens,
pagination/search, extra unit tests, health-based container healthchecks, or the
PostgreSQL cloud variant (Appendix).

---

## 7. Discussion questions

1. Why does the Product service validate the JWT **locally** instead of calling the Auth
   service on every request? What are the trade-offs?
2. What breaks if the two services use **different** signing keys? Why?
3. The gateway forwards the `Authorization` header but doesn't validate it. Should it?
   What would validating at the gateway buy you, and what would it cost?
4. We gave each service its **own** database. When would sharing one database be
   tempting, and why is it discouraged in microservices?
5. Why is "build once, push to a registry, deploy the same image everywhere" better than
   letting each environment build from source?
6. The React `ProtectedRoute` hides the products page from anonymous users. Is that
   *security*? Where does the real enforcement happen?
7. Why are secrets (keys, connection strings) provided via **environment variables**
   rather than committed to the repo?
8. On the free tier, services "sleep". How would you handle cold starts in production?

---

## 8. Live-demo script (5 minutes, for lecturing)

1. `GET http://localhost:7000/api/products` (no token) → **401**. *"Locked, even through the gateway."*
2. `POST http://localhost:7000/api/auth/login` → copy token. *"Same URL, different service — that's the gateway routing."*
3. Paste token at jwt.io → show the readable claims. *"Signed, not secret."*
4. Repeat the products call with `Authorization: Bearer …` → **200**.
5. Open the React app (:7173 or :7080) → register → add a product → refresh → it persists.
6. `docker compose up --build` → same app, one command, five containers.
7. (If deployed) Show the Docker Hub repos + the Actions run that pushed them.

---

## 9. Extensions / homework ideas

- Add **rate limiting** and **response caching** on the gateway (Ocelot supports both in `ocelot.json`).
- Add a **Categories** entity and a relationship to Products.
- Add **search + pagination** to `GET /api/products`.
- Add **roles** (admin vs user) to the JWT and restrict delete to admins.
- Add **integration tests** that spin the stack up with Testcontainers.
- Swap SQL Server for **PostgreSQL** on the cloud (README Appendix) and compare.
- Replace Ocelot with **YARP** and compare configuration styles.
