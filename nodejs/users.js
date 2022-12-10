let mysql = require('mysql');
let uuid = require('uuid');

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


exports.login = (req, res) => {

	if (req.body?.username == undefined || req.body?.password == undefined ) {
		let json = {
			error: 1,
			message: 'Missing username or password'
		}
		res.send(json);
		return;
	}


	let dbConnection = GetConnection();

	dbConnection.connect( (err) => {
		if(err) {
			let json = {
				error: 1,
				message: err
			};
			res.send(json);
			return;
		}

		let query = "SELECT * from users where username = '" + req.body.username + "' and password = sha2('" + req.body.password + "', 512);";

		dbConnection.query(query, (err, result) => {
			if (err) {
				let json = {
					error: 1,
					message: err
				};
				res.send(json);
				return;
			}

			if (result.result < 1) {
				let json = {
					error: 1,
					message: "Incorrect username and/or password."
				};
				res.send(json);
				return;
			}
			let json = {
				error: 0,
				message: "Successfully logged in",
				token: GenerateToken(result[0].id, req, res)
			}

			res.send(json);

		});

	});

};



function GenerateToken(user_id, req, res) {
	let token = uuid.v4();
	let dbConnection = GetConnection();

	dbConnection.connect( (err) => {
		if(err) {
			let json = {
				error: 1,
				message: err
			};
			res.send(json);
			return;
		}

		let query = "INSERT INTO tokens(user_id, token) VALUES('" + user_id + "', '" + token +"')";

		dbConnection.query(query, (err, result) => {
			if (err) {
				let json = {
					error: 1,
					message: err
				};
				res.send(json);
				return;
			}
		});

	});

	return token;
}