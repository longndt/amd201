// Tiny fetch wrapper for talking to the two microservices.
const AUTH_URL = import.meta.env.VITE_AUTH_API_URL || 'http://localhost:5001'
const PRODUCT_URL = import.meta.env.VITE_PRODUCT_API_URL || 'http://localhost:5002'

function getToken() {
  return localStorage.getItem('token')
}

async function request(baseUrl, path, { method = 'GET', body, auth = false } = {}) {
  const headers = { 'Content-Type': 'application/json' }
  if (auth) {
    const token = getToken()
    if (token) headers['Authorization'] = `Bearer ${token}`
  }

  const res = await fetch(`${baseUrl}${path}`, {
    method,
    headers,
    body: body ? JSON.stringify(body) : undefined,
  })

  if (res.status === 204) return null // No Content (e.g. DELETE)

  const data = await res.json().catch(() => ({}))
  if (!res.ok) {
    throw new Error(data.message || `Request failed (${res.status})`)
  }
  return data
}

// --- Auth service ---
export const authApi = {
  register: (payload) => request(AUTH_URL, '/api/auth/register', { method: 'POST', body: payload }),
  login: (payload) => request(AUTH_URL, '/api/auth/login', { method: 'POST', body: payload }),
}

// --- Product service (all require a JWT) ---
export const productApi = {
  list: () => request(PRODUCT_URL, '/api/products', { auth: true }),
  create: (payload) => request(PRODUCT_URL, '/api/products', { method: 'POST', body: payload, auth: true }),
  update: (id, payload) => request(PRODUCT_URL, `/api/products/${id}`, { method: 'PUT', body: payload, auth: true }),
  remove: (id) => request(PRODUCT_URL, `/api/products/${id}`, { method: 'DELETE', auth: true }),
}
