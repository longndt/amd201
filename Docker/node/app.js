const http = require('http')
const server = http.createServer((req, res) => {
   res.writeHead(200, { 'Content-Type': 'text/plain' })
   res.write("Hello Docker from Greenwich Vietnam")
   res.end()
})
const PORT = 3000
server.listen(PORT, () => {
   console.log(`Server running at http://localhost:${PORT}/`)
})