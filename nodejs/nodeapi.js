let express = require('express');
const bodyParser = require('body-parser');
let drawings = require('./puzzles.js');
let user = require('./users.js');

let app = express();

app.use(bodyParser.urlencoded({extended: true}));

//Login

app.post('/login', (req, res) => {
	res.set('Content-Type', 'application/json');
	user.login(req, res);
});

//Register

app.post('/register', (req, res) => {
	res.set('Content-Type', 'application/json');
	user.register(req, res);
});

// Get all drawings
app.get('/drawings', (req, res) => {
	res.set('Content-Type', 'application/json');
	drawings.getdrawings(req, res);
});

//Get single drawing
app.get('/drawing', (req, res) => {
	res.set('Content-type', 'application/json');
	drawings.getSingleDrawing(req, res);
});

// Insert new drawing
app.post('/drawing', (req, res) => {
	res.set('Content-Type', 'application/json');
	drawings.newdrawing(req, res);
});

// Update drawing

app.put('/drawing', (req, res) => {
	res.set('Content-Type', 'application/json');
	drawings.UpdateDrawing(req, res);
});

// Delete drawing
app.delete('/drawing', (req, res) => {
	res.set('Content-Type', 'application/json');
	drawings.deleteDrawing(req, res);
});


let server = app.listen(8080, () => {
	let host = server.address().address
	let port = server.address().port
	console.log("API elindítva: http://%s:%s", host, port)
  })