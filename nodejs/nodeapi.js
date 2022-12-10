let express = require('express');
let puzzles = require('./puzzles.js'); 

let app = express();

app.get('/drawings', (req, res) => {
	puzzles.getdrawings(req, res);
});

let server = app.listen(8080, () => {
	let host = server.address().address
	let port = server.address().port
	console.log("API elindítva: http://%s:%s", host, port)
  })