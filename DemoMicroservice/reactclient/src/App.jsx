import './App.css'
import Greenwich from './Greenwich'
import Laptop from './Laptop'

function App() {

  return (
      <>
          <div className="container text-center">
              <div className="row"> 
                  <div className="col-md-8 card">
                      <Laptop />
                  </div>
                  <div className="col-md-4 card">
                      <Greenwich />
                  </div>
              </div>
          </div>
   
    </>
  )
}

export default App
