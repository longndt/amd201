import axios from 'axios'
import { useState, useEffect } from 'react'

const Laptop = () => {
    const api = 'https://localhost:9999/laptops'
    const [laptops, setLaptops] = useState([])
    const [error, setError] = useState(null)

    useEffect(() => {
        const fetchData = async () => {
            try {
                const { data } = await axios.get(api)
                setLaptops(data)
            } catch (err) {
                setError(err.message)
            }
        }

        fetchData()
    }, [])

    if (error) {
        return <div>Error: {error}</div>
    }

    return (
        <div className="container text-center">
            <table className="table table-bordered mt-5">
                <thead>
                    <tr>
                        <th colSpan="3" className="h4 text-primary">LAPTOP LIST</th>
                    </tr>
                    <tr>
                        <th>Laptop Id</th>
                        <th>Laptop Name</th>
                        <th>Laptop Color</th>
                        <th>Laptop Quantity</th>
                        <th>Laptop Image</th>
                    </tr>
                </thead>
                <tbody>
                    {laptops.map((laptop) => (
                        <tr key={laptop.id}>
                            <td>{laptop.id}</td>
                            <td>{laptop.name}</td>
                            <td>{laptop.color}</td>
                            <td>{laptop.quantity}</td>
                            <td>
                                <img src={laptop.image} alt="Laptop image" width="100" height="120" />
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default Laptop