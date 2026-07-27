import { useEffect, useState } from 'react'
import { productApi } from '../api.js'

const EMPTY = { name: '', description: '', price: '', stock: '' }

export default function Products() {
  const [products, setProducts] = useState([])
  const [form, setForm] = useState(EMPTY)
  const [editingId, setEditingId] = useState(null)
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(true)

  async function load() {
    setLoading(true)
    setError('')
    try {
      setProducts(await productApi.list())
    } catch (err) {
      setError(err.message)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => { load() }, [])

  function update(e) {
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  function startEdit(p) {
    setEditingId(p.id)
    setForm({ name: p.name, description: p.description ?? '', price: p.price, stock: p.stock })
  }

  function cancelEdit() {
    setEditingId(null)
    setForm(EMPTY)
  }

  async function handleSubmit(e) {
    e.preventDefault()
    setError('')
    const payload = {
      name: form.name,
      description: form.description,
      price: parseFloat(form.price) || 0,
      stock: parseInt(form.stock, 10) || 0,
    }
    try {
      if (editingId) {
        await productApi.update(editingId, payload)
      } else {
        await productApi.create(payload)
      }
      cancelEdit()
      await load()
    } catch (err) {
      setError(err.message)
    }
  }

  async function handleDelete(id) {
    if (!confirm('Delete this product?')) return
    setError('')
    try {
      await productApi.remove(id)
      await load()
    } catch (err) {
      setError(err.message)
    }
  }

  return (
    <div className="products">
      <div className="card">
        <h2>{editingId ? 'Edit product' : 'Add product'}</h2>
        <form onSubmit={handleSubmit} className="product-form">
          <input name="name" placeholder="Name" value={form.name} onChange={update} required />
          <input name="description" placeholder="Description" value={form.description} onChange={update} />
          <input name="price" placeholder="Price" type="number" step="0.01" min="0" value={form.price} onChange={update} required />
          <input name="stock" placeholder="Stock" type="number" min="0" value={form.stock} onChange={update} required />
          <div className="form-actions">
            <button className="btn btn-primary">{editingId ? 'Update' : 'Add'}</button>
            {editingId && <button type="button" className="btn btn-ghost" onClick={cancelEdit}>Cancel</button>}
          </div>
        </form>
      </div>

      {error && <div className="alert error">{error}</div>}

      <div className="card">
        <h2>Products</h2>
        {loading ? (
          <p className="muted">Loading…</p>
        ) : products.length === 0 ? (
          <p className="muted">No products yet. Add your first one above.</p>
        ) : (
          <table className="table">
            <thead>
              <tr><th>ID</th><th>Name</th><th>Description</th><th>Price</th><th>Stock</th><th></th></tr>
            </thead>
            <tbody>
              {products.map((p) => (
                <tr key={p.id}>
                  <td>{p.id}</td>
                  <td>{p.name}</td>
                  <td>{p.description}</td>
                  <td>${Number(p.price).toFixed(2)}</td>
                  <td>{p.stock}</td>
                  <td className="row-actions">
                    <button className="btn btn-small" onClick={() => startEdit(p)}>Edit</button>
                    <button className="btn btn-small btn-danger" onClick={() => handleDelete(p.id)}>Delete</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  )
}
