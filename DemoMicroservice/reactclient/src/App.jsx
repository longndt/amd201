import './App.css'
import axios from 'axios'
import { useState, useEffect } from 'react'

function App() {
    const apiEndpoint = 'https://localhost:8888/gateway/laptops'
    const [laptops, setLaptops] = useState([])
    const [error, setError] = useState(null)
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        const fetchData = async () => {
            setIsLoading(true)
            try {
                const { data } = await axios.get(apiEndpoint)
                setLaptops(data)
            } catch (err) {
                setError(err.message)
            } finally {
                setIsLoading(false)
            }
        }

        fetchData()
    }, [])

    const handleDelete = async (id) => {
        const confirmed = window.confirm('Are you sure you want to delete this laptop?')
        if (!confirmed) return
        try {
            await axios.delete(`${apiEndpoint}/${id}`)
            setLaptops(laptops.filter(laptop => laptop.id !== id))
        } catch (err) {
            setError(err.message)
        }
    }

    if (isLoading) {
        return <div className="center-align" style={{ marginTop: '50px' }}>Loading...</div>
    }

    if (error) {
        return <div className="center-align" style={{ marginTop: '50px' }}>Error: {error}</div>
    }

    return (
        <div className="container">
            <div className="row">
                <div className="col s12">
                    <h1 className="center-align">LAPTOP LIST</h1>
                </div>
            </div>
            <div className="row">
                <div className="col s12">
                    <div className="center-align">
                        <table className="striped responsive-table centered">
                            <thead>
                                <tr>
                                    <th>Laptop Id</th>
                                    <th>Laptop Brand</th>
                                    <th>Laptop Model</th>
                                    <th>Laptop Quantity</th>
                                    <th>Laptop Price</th>
                                    <th>Menu</th>
                                </tr>
                            </thead>
                            <tbody>
                                {laptops.map((laptop) => (
                                    <tr key={laptop.id}>
                                        <td>{laptop.id}</td>
                                        <td>{laptop.brand}</td>
                                        <td>{laptop.model}</td>
                                        <td>{laptop.quantity}</td>
                                        <td>{laptop.price}$</td>
                                        <td>
                                            <button
                                                className="waves-effect waves-light btn-large red"
                                                onClick={() => handleDelete(laptop.id)}
                                                type="button"
                                            >
                                                DELETE
                                            </button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default App