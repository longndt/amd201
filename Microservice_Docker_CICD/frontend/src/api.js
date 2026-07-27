// Tiny fetch wrapper. The browser talks ONLY to the API Gateway (Ocelot),
// which routes /api/auth/* to the Auth service and /api/products/* to the Product service.
const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:7000'

function getToken() {
  return localStorage.getItem('token')
}

async function request(path, { method = 'GET', body, auth = false } = {}) {
  const headers = { 'Content-Type': 'application/json' }
  if (auth) {
    const token = getToken()
    if (token) headers['Authorization'] = `Bearer ${token}`
  }

  const res = await fetch(`${API_URL}${path}`, {
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

// --- Auth (routed by the gateway to the Auth service) ---
export const authApi = {
  register: (payload) => request('/api/auth/register', { method: 'POST', body: payload }),
  login: (payload) => request('/api/auth/login', { method: 'POST', body: payload }),
}

// --- Products (routed by the gateway to the Product service; all require a JWT) ---
export const productApi = {
  list: () => request('/api/products', { auth: true }),
  create: (payload) => request('/api/products', { method: 'POST', body: payload, auth: true }),
  update: (id, payload) => request(`/api/products/${id}`, { method: 'PUT', body: payload, auth: true }),
  remove: (id) => request(`/api/products/${id}`, { method: 'DELETE', auth: true }),
}
