namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Threading.Tasks;

	public class ConnectionFactory
		: IConnectionFactory
	{
		public readonly String connectionString;

		public ConnectionFactory(String connectionString)
		{
			this.connectionString = connectionString;
		}

		public async Task<NpgsqlConnection> CreateAsync()
		{
			var npgsqlConnection = new NpgsqlConnection(connectionString);

			await npgsqlConnection.OpenAsync();

			return npgsqlConnection;
		}
	}
}
