let express = require('express');
const bodyParser = require('body-parser');
let puzzles = require('./puzzles.js');
let user = require('./users.js');

let app = express();

app.use(bodyParser.urlencoded({extended: true}));

app.get('/drawings', (req, res) => {
	res.set('Content-Type', 'application/json');
	puzzles.getdrawings(req, res);
});

//LOGIN

app.post('/login', (req, res) => {
	res.set('Content-Type', 'application/json');
	user.login(req, res);
});

//Register

app.post('/register', (req, res) => {
	res.set('Content-Type', 'application/json');
	user.register(req, res);
});

let server = app.listen(8080, () => {
	let host = server.address().address
	let port = server.address().port
	console.log("API elindítva: http://%s:%s", host, port)
  })