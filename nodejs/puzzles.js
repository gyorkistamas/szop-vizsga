let mysql = require('mysql');

function GetConnection() {
	let dbConnection = mysql.createConnection({
		host: '127.0.0.1',
		user: 'main',
		password: 'password',
		port: 3306,
		database: 'szop_vizsga_pixeldraw',
	});

	return dbConnection;
}



exports.getdrawings = (req, res) => {
	dbConnection = GetConnection();
	dbConnection.connect( (err) => {
		if(err) {
			let json = {
				error: 1,
				message: err
			};
			res.status(400);
			res.send(json);
			return;
		}

		let query = 'SELECT d.id, u.username, d.title, d.drawing_data FROM drawings d left join users u on u.id = d.user_id';

		dbConnection.query(query, (err, result) => {
			if (err) {
				let json = {
					error: 1,
					message: err
				};
				res.status(400);
				res.send(json);
				return;
			}

			let json = {
				error: 0,
				message: 'Listing drawings.',
				data: result
			};

			res.send(json);
			return;

		});

	});
}

exports.getSingleDrawing = (req, res) => {
	if (req.query?.drawing_id == undefined) {
		let response = {
			error: 1,
			message: 'Missing parameters'
		}
		res.status(400);
		res.send(response);
		return;
	}


	let conn = GetConnection();

	let query = "SELECT d.id, u.username, d.title, d.drawing_data FROM drawings d left join users u on u.id = d.user_id WHERE d.id = '" + req.query.drawing_id + "';";
	conn.query(query, (error, result) => {
		if (error) {
			let response = {
				error:1,
				message: error
			}
			res.status(400);
			res.send(response);
			return;
		}

		if (result[0]?.id == undefined) {
			let response = {
				error:1,
				message: 'Drawing not found!'
			}
			res.status(404);
			res.send(response);
			return;
		}

		let response = {
			error: 0,
			message: 'Drawing data:',
			data: result[0]
		}

		res.send(response);

	});

};

exports.newdrawing = (req, res) => {
	if (req.body?.token == undefined || req.body?.title == undefined || req.body?.drawing_data == undefined) {
		let response = {
			error: 1,
			message: 'Missing parameters!'
		}
		res.status(400);
		res.send(response);
		return;
	}

	if (req.body.drawing_data.length != 100 || isNaN(req.body.drawing_data)) {
		let response = {
			error:1,
			message: 'Drawing data incorrect!',
			length: req.body.drawing_data.length,
		}
		res.status(400);
		res.send(response);
		return;
	}

	let conn = GetConnection();
	let query = "SELECT user_id FROM tokens WHERE token = '" + req.body.token + "' AND expire_date > now();"
	conn.query(query, (error, response) => {
		if (error) {
			let response = {
				error: 1,
				message: error
			}
			res.send(response);
			return;
		}

		let id = response[0]?.user_id;

		if (!id) {
			let response = {
				error: 1,
				message: 'Token invalid'
			}
			res.status(403);
			res.send(response);
			return;
		}

		let insertQuery = "INSERT INTO drawings(user_id, title, drawing_data) VALUES (" + id + ", '" + req.body.title + "', '" + req.body.drawing_data + "');";
		conn.query(insertQuery, (error, response) => {
			if (error) {
				let response = {
					error: 1,
					message: error
				}
				res.send(response);
				return;
			}

			let response2 = {
				error: 0,
				message: 'Drawing sucessfully uploaded!'
			};
			res.status(201);
			res.send(response2);

		});

	});
};