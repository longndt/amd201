import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { authApi } from '../api.js'
import { useAuth } from '../context/AuthContext.jsx'

export default function Register() {
  const { login } = useAuth()
  const navigate = useNavigate()
  const [form, setForm] = useState({ username: '', email: '', password: '' })
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  function update(e) {
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  async function handleSubmit(e) {
    e.preventDefault()
    setError('')
    setLoading(true)
    try {
      const res = await authApi.register(form)
      login(res) // auto-login after successful registration
      navigate('/')
    } catch (err) {
      setError(err.message)
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="card auth-card">
      <h2>Create account</h2>
      {error && <div className="alert error">{error}</div>}
      <form onSubmit={handleSubmit}>
        <label>Username
          <input name="username" value={form.username} onChange={update} minLength={3} required />
        </label>
        <label>Email
          <input name="email" type="email" value={form.email} onChange={update} required />
        </label>
        <label>Password
          <input name="password" type="password" value={form.password} onChange={update} minLength={6} required />
        </label>
        <button className="btn btn-primary" disabled={loading}>
          {loading ? 'Creating…' : 'Register'}
        </button>
      </form>
      <p className="muted">Already have an account? <Link to="/login">Login</Link></p>
    </div>
  )
}
