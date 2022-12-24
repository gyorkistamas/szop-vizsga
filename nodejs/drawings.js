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
				message: err.code
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
					message: err.code
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
				message: error.code
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
				message: error.code
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
					message: error.code
				}
				res.send(response);
				return;
			}

			let response2 = {
				error: 0,
				message: 'Drawing successfully uploaded!'
			};
			res.status(201);
			res.send(response2);

		});

	});
};



exports.UpdateDrawing = (req, res) => {
	if (req.body?.token == undefined || req.body?.id == undefined || req.body?.title == undefined || req.body?.drawing_data == undefined) {
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
			message: 'Drawing data incorrect!'
		}
		res.status(400);
		res.send(response);
		return;
	}

	if (req.body.title.length == 0) {
		let response = {
			error:1,
			message: 'Title cannot be empty!'
		}
		res.status(400);
		res.send(response);
		return;
	}

	let connection = GetConnection();

	let query = "select (select tokens.user_id from tokens where token = '" + req.body.token + "' AND expire_date > now()) = (select drawings.user_id from drawings where id = " + req.body.id + ") as 'equal' from dual;";

	connection.query(query, (error, response) => {
		if (error) {
			let json = {
				error: 1,
				message: error.code
			}
			res.send(json);
			return;
		}

		if (response[0].equal != 1) {
			let json = {
				error: 1,
				message: 'You can only update your own drawing!'
			}
			res.send(json);
			return;
		}

		let updatequery = "UPDATE drawings set title = '" + req.body.title + "', drawing_data = '" + req.body.drawing_data + "' WHERE id = " + req.body.id + ";";
		connection.query(updatequery, (error, response) => {
			if (error) {
				let json = {
					error: 1,
					message: error.code
				}
				res.send(json);
				return;
			}

			let json = {
				error: 0,
				message: 'Drawing updated successfully!'
			};

			res.send(json);


		});



	});

};


exports.DeleteDrawing = (req, res) => {
	if (req.body?.token == undefined || req.body?.id == undefined) {
		let response = {
			error: 1,
			message: 'Missing parameters!'
		}
		res.status(400);
		res.send(response);
		return;
	}

	let connection = GetConnection();

	let query = "select (select tokens.user_id from tokens where token = '" + req.body.token + "' AND expire_date > now()) = (select drawings.user_id from drawings where id = " + req.body.id + ") as 'equal' from dual;";

	connection.query(query, (error, response) => {
		if (error) {
			let json = {
				error: 1,
				message: error.code
			}
			res.send(json);
			return;
		}

		if (response[0].equal != 1) {
			let json = {
				error: 1,
				message: 'You can only delete your own drawing!'
			}
			res.send(json);
			return;
		}

		let updatequery = "delete from drawings where id = '" + req.body.id + "'";
		connection.query(updatequery, (error, response) => {
			if (error) {
				let json = {
					error: 1,
					message: error.code
				}
				res.send(json);
				return;
			}

			let json = {
				error: 0,
				message: 'Drawing updated successfully!'
			};

			res.send(json);


		});



	});

};