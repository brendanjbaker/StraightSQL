namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
		: IQueryDispatcher
	{
		private readonly ICommandPreparer commandPreparer;
		private readonly IConnectionFactory connectionFactory;

		public QueryDispatcher(ICommandPreparer commandPreparer, IConnectionFactory connectionFactory)
		{
			if (commandPreparer == null)
				throw new ArgumentNullException(nameof(commandPreparer));

			if (connectionFactory == null)
				throw new ArgumentNullException(nameof(connectionFactory));

			this.commandPreparer = commandPreparer;
			this.connectionFactory = connectionFactory;
		}

		private async Task<T> ExecuteQueryAsync<T>(IQuery query, Func<NpgsqlCommand, Task<T>> functionAsync)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					commandPreparer.Prepare(command, query);

					return await functionAsync(command);
				}
			}
		}
	}
}
