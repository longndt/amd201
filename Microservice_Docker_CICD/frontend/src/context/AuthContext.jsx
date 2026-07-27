import { createContext, useContext, useEffect, useState } from 'react'

const AuthContext = createContext(null)

// Provides the current user + login/logout helpers to the whole app.
export function AuthProvider({ children }) {
  const [user, setUser] = useState(null)

  // Restore the session from localStorage on page reload.
  useEffect(() => {
    const raw = localStorage.getItem('user')
    if (raw) setUser(JSON.parse(raw))
  }, [])

  function login(authResponse) {
    // authResponse = { username, email, token, expiresAt }
    localStorage.setItem('token', authResponse.token)
    localStorage.setItem('user', JSON.stringify({
      username: authResponse.username,
      email: authResponse.email,
    }))
    setUser({ username: authResponse.username, email: authResponse.email })
  }

  function logout() {
    localStorage.removeItem('token')
    localStorage.removeItem('user')
    setUser(null)
  }

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  return useContext(AuthContext)
}
