let express = require('express');
const bodyParser = require('body-parser');
let drawings = require('./drawings.js');
let user = require('./users.js');
let http = require('http');
let swaggerUI = require("swagger-ui-express"),swaggerDocument = require('./openapi.json');


let app = express();

app.use(bodyParser.urlencoded({extended: true}));

app.use("/api-docs", swaggerUI.serve, swaggerUI.setup(swaggerDocument));


app.get('/', (req, res) => {
	res.redirect('/api-docs');
});

//Login

app.post('/login', (req, res) => {
	res.set('Content-Type', 'application/json');
	try {
		user.login(req, res);
	}
	catch(error) {
		let errorResponse = {
			error: 1,
			message: `Error, please try again later (${error})`
		}
		res.send(errorResponse);
	}
});

//Register

app.post('/register', (req, res) => {
	res.set('Content-Type', 'application/json');
	try {
		user.register(req, res);
	}
	catch(error) {
		let errorResponse = {
			error: 1,
			message: `Error, please try again later (${error})`
		}
		res.send(errorResponse);
	}
});

// Get all drawings
app.get('/drawings', (req, res) => {
	res.set('Content-Type', 'application/json');
	try {
		drawings.getdrawings(req, res);
	}
	catch(error) {
		let errorResponse = {
			error: 1,
			message: `Error, please try again later (${error})`
		}
		res.send(errorResponse);
	}
});

//Get single drawing
app.get('/drawing/:id?', (req, res) => {
	res.set('Content-type', 'application/json');
	try {
		drawings.getSingleDrawing(req, res);
	}
	catch(error) {
		let errorResponse = {
			error: 1,
			message: `Error, please try again later (${error})`
		}
		res.send(errorResponse);
	}
});

// Insert new drawing
app.post('/drawing', (req, res) => {
	res.set('Content-Type', 'application/json');
	try {
		drawings.newdrawing(req, res);
	}
	catch(error) {
		let errorResponse = {
			error: 1,
			message: `Error, please try again later (${error})`
		}
		res.send(errorResponse);
	}
});

// Update drawing

app.put('/drawing/:id?', (req, res) => {
	res.set('Content-Type', 'application/json');
	try {
		drawings.UpdateDrawing(req, res);
	}
	catch(error) {
		let errorResponse = {
			error: 1,
			message: `Error, please try again later (${error})`
		}
		res.send(errorResponse);
	}
});

// Delete drawing
app.delete('/drawing/:token?/:id?', (req, res) => {
	res.set('Content-Type', 'application/json');
	try {
		drawings.DeleteDrawing(req, res);
	}
	catch(error) {
		let errorResponse = {
			error: 1,
			message: `Error, please try again later (${error})`
		}
		res.send(errorResponse);
	}
});

// Get data from PHP API
app.get('/php', (req, res) => {
	res.set('Content-Type', 'application/json');

	let options = {
		host: 'localhost',
		path: '/szop_beadando/grades.php?username=' + req.query.username + '&password=' + req.query.password
	}

	try {
		let request = http.get(options, (response) => {
			let data = '';
	
			response.on('data', (chunck) => {
				data += chunck;
			});
	
			response.on('end', () => {
				res.send(data);
			});
	
		}).end();


		request.on("error", (error) => {
			let errorResponse = {
				error: 1,
				message: `Error, please try again later (${error})`
			}
			res.send(errorResponse);
		});
	}
	catch(error) {
		let errorResponse = {
			error: 1,
			message: `Error, please try again later (${error})`
		}
		res.send(errorResponse);
	}

});

app.get('/*', (req, res) => {
	res.set('Content-Type', 'application/json');
	let json = {
		error: 1,
		message: 'Invalid URL'
	};
	res.send(json);
});

app.post('/*', (req, res) => {
	res.set('Content-Type', 'application/json');
	let json = {
		error: 1,
		message: 'Invalid URL'
	};
	res.send(json);
});

app.put('/*', (req, res) => {
	res.set('Content-Type', 'application/json');
	let json = {
		error: 1,
		message: 'Invalid URL'
	};
	res.send(json);
});

app.delete('/*', (req, res) => {
	res.set('Content-Type', 'application/json');
	let json = {
		error: 1,
		message: 'Invalid URL'
	};
	res.send(json);
});


let server = app.listen(8080, () => {
	let host = server.address().address
	let port = server.address().port
	console.log("API elindítva: http://%s:%s", host, port)
  })