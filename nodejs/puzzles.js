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
			res.send(json);
			return;
		}

		let query = 'SELECT d.id, u.username, d.description, d.drawing_data FROM drawings d left join users u on u.id = d.user_id';

		dbConnection.query(query, (err, result) => {
			if (err) {
				let json = {
					error: 1,
					message: err
				};
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