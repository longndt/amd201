# Instructor Guide

Companion to [`README.md`](README.md) (the student lab). This guide helps you run the
session: timing, talking points, checkpoints, the mistakes students actually make, a
grading rubric, and discussion questions.

---

## 1. At a glance

| | |
|---|---|
| **Audience** | Students with basic C#, JavaScript, and HTTP knowledge |
| **Prereqs to verify beforehand** | .NET 8 SDK, Node 20+, Docker Desktop, Git, GitHub account |
| **Delivery** | Guided walkthrough + independent build, or flipped (students read README first) |
| **Total time** | ~3–4 hours (can split across 2 sessions) |
| **Deliverable** | Public GitHub repo + live Render URLs + short demo |

### Recommended split across two sessions
- **Session 1 (2h):** Parts A–E — the two services + React running locally.
- **Session 2 (2h):** Parts F–H — Docker, CI/CD, Render deploy.

---

## 2. Suggested timing

| Segment | Time | Notes |
|--------|------|-------|
| Intro + architecture (slides) | 20 min | Use `docs/slides.html`. Emphasize the JWT-across-services idea. |
| Part A — SQL Server in Docker | 10 min | Everyone runs the same `docker run`. Confirm `docker ps`. |
| Part B — Auth service | 35 min | Walk the code, then let them run + register in Swagger. |
| Part C — Product service | 35 min | Focus on `[Authorize]` and JWT validation config. |
| **Checkpoint 1** | 5 min | Everyone has a token and gets 200 on `GET /products`. |
| Part D — React frontend | 40 min | Env vars, AuthContext, protected routes. |
| Part E — full local run | 15 min | Register → login → CRUD end-to-end. |
| **Checkpoint 2** | 5 min | Working app in the browser. |
| Part F — Docker Compose | 25 min | One command; explain the `sqlserver` hostname gotcha. |
| Part G — CI/CD | 25 min | Push to GitHub, watch Actions. |
| Part H — Render deploy | 40 min | The slowest part; free tier builds are slow. |
| Wrap-up + Q&A | 15 min | Discussion questions below. |

---

## 3. Talking points per part (the "why")

**Architecture.** Ask: *"Why two services instead of one?"* Draw out independent
deploy/scaling and separate databases. Show `docs/images/architecture.svg`.

**Auth service.** The big idea is **we never store raw passwords**. Open
`AuthController.HashPassword` and explain salt + iterations. Then explain the JWT: it's
a signed statement "this is user X", not encryption — anyone can read the payload, but
nobody can forge it without the key. Show https://jwt.io by pasting a token.

**Product service.** The `[Authorize]` attribute + `AddJwtBearer` config. Stress the
**shared key** rule (`docs/images/jwt-flow.svg`). Demo the 401 first, then authorize,
then 200 — the "aha" moment.

**React.** Three teaching points: (1) env vars are **build-time** in Vite; (2) the token
lives in `localStorage` and is attached in `api.js`; (3) `ProtectedRoute` gates the UI,
but **the real security is server-side** — the UI guard is just UX.

**Docker.** Multi-stage builds (build image vs runtime image) and *why* images stay
small. The `sqlserver` vs `localhost` hostname difference inside the compose network is
the #1 confusion — call it out explicitly.

**CI/CD.** "A robot builds your code on every push so problems surface early." Map the
four jobs to the pipeline figure `docs/images/cicd-flow.svg`.

**Render.** Real cloud deploy. Reinforce that config (connection strings, keys, CORS)
comes from **environment variables**, not committed to the repo.

---

## 4. Checkpoints (verify before moving on)

1. **After Part A:** `docker ps` shows `lab-sqlserver`.
2. **After Part C:** In Product Swagger, `GET /api/products` returns **401** without a
   token and **200** after Authorize. *(This proves cross-service JWT works.)*
3. **After Part E:** Full register → login → create/edit/delete in the browser.
4. **After Part F:** `docker compose up --build` serves the app at `localhost:8080`.
5. **After Part G:** Green (or intentionally-red-at-deploy) Actions run.
6. **After Part H:** Public Render frontend URL works end-to-end.

---

## 5. Common student mistakes (and the fix)

| Mistake | Symptom | Fix |
|--------|---------|-----|
| JWT settings differ between services | Product API always 401 | Copy the **exact** `Jwt__Key/Issuer/Audience` to both |
| Using `localhost` for DB inside compose | Service can't reach DB in Docker | Use host `sqlserver` in compose connection strings |
| Forgetting CORS after deploy | Browser blocks API calls on Render | Set `AllowedOrigins` to the frontend URL, redeploy |
| Editing `.env` and expecting live change | Frontend still hits old API | Vite bakes env at build; restart dev / rebuild image |
| Weak SA password | SQL Server container won't start | 8+ chars incl. upper, lower, digit |
| Wrong Dockerfile path on Render | Build fails immediately | Root dir = `services/AuthService` etc. |
| Pasting `Bearer ` prefix in Swagger Authorize | Still 401 | Paste the **raw** token only |
| Port already in use | `dotnet run` / compose fails to bind | Stop the other process or change the port |

---

## 6. Grading rubric (100 pts)

| Criterion | Pts | What to look for |
|----------|-----|------------------|
| Auth service works | 15 | Register + login return a JWT; passwords hashed |
| Product CRUD works | 15 | All four operations; correct status codes |
| Auth enforced across services | 15 | 401 without token, 200 with valid token |
| React frontend | 15 | Login/register + CRUD UI; token handling; protected route |
| Dockerization | 10 | Valid Dockerfiles; `docker compose up` runs the stack |
| CI/CD pipeline | 10 | Actions builds services + images on push |
| Render deployment | 10 | Live, reachable frontend backed by both services |
| Code quality & structure | 5 | DTOs, separation, no secrets committed |
| Documentation / demo | 5 | README notes, screenshots, or short walkthrough |

**Bonus (up to +10):** input validation & error UX, refresh tokens, pagination/search,
unit tests, health-based container healthchecks, or PostgreSQL cloud variant (Appendix).

---

## 7. Discussion questions

1. Why does the Product service validate the JWT **locally** instead of calling the Auth
   service on every request? What are the trade-offs?
2. What breaks if the two services use **different** signing keys? Why?
3. We gave each service its **own** database. When would sharing one database be tempting,
   and why is it discouraged in microservices?
4. The React `ProtectedRoute` hides the products page from anonymous users. Is that
   *security*? Where does the real enforcement happen?
5. Why are secrets (keys, connection strings) provided via **environment variables**
   rather than committed to the repo?
6. On the free tier, services "sleep". How would you handle cold starts in production?

---

## 8. Live-demo script (5 minutes, for lecturing)

1. Open Product Swagger → `GET /api/products` → **401**. *"It's locked."*
2. Open Auth Swagger → `register` → copy token.
3. Paste token at jwt.io → show the readable claims. *"Signed, not secret."*
4. Back in Product Swagger → **Authorize** → `GET /api/products` → **200**.
5. Open the React app → register → add a product → refresh → it persists.
6. `docker compose up --build` → same app, one command.

---

## 9. Extensions / homework ideas

- Add a **Categories** entity and a relationship to Products.
- Add **search + pagination** to `GET /api/products`.
- Add **roles** (admin vs user) to the JWT and restrict delete to admins.
- Add **unit/integration tests** and a test job in CI.
- Add an **API gateway** (YARP) in front of both services.
- Swap SQL Server for **PostgreSQL** on the cloud (README Appendix) and compare.
