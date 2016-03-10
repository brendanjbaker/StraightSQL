namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
		: IQueryDispatcher
	{
		private readonly IConnectionFactory connectionFactory;

		public QueryDispatcher(IConnectionFactory connectionFactory)
		{
			if (connectionFactory == null)
				throw new ArgumentNullException(nameof(connectionFactory));

			this.connectionFactory = connectionFactory;
		}

		private static void PrepareCommand(NpgsqlCommand npgsqlCommand, IQuery query)
		{
			if (npgsqlCommand == null)
				throw new ArgumentNullException(nameof(npgsqlCommand));

			if (query == null)
				throw new ArgumentNullException(nameof(query));

			npgsqlCommand.CommandText = query.Text;

			foreach (var queryParameter in query.Parameters)
			{
				npgsqlCommand.Parameters.Add(queryParameter);
			}
		}

		private async Task<T> ExecuteQueryAsync<T>(IQuery query, Func<NpgsqlCommand, Task<T>> functionAsync)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					PrepareCommand(command, query);

					return await functionAsync(command);
				}
			}
		}
	}
}
