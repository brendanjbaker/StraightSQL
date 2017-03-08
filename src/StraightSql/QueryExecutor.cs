namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Threading.Tasks;

	public class QueryExecutor
		: IQueryExecutor
	{
		private readonly ICommandPreparer commandPreparer;
		private readonly IConnectionFactory connectionFactory;

		public QueryExecutor(ICommandPreparer commandPreparer, IConnectionFactory connectionFactory)
		{
			if (commandPreparer == null)
				throw new ArgumentNullException(nameof(commandPreparer));

			if (connectionFactory == null)
				throw new ArgumentNullException(nameof(connectionFactory));

			this.commandPreparer = commandPreparer;
			this.connectionFactory = connectionFactory;
		}

		public async Task<T> ExecuteQueryAsync<T>(IQuery query, Func<NpgsqlCommand, Task<T>> functionAsync)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					commandPreparer.Prepare(command, query);

					try
					{
						return await functionAsync(command);
					}
					catch (PostgresException pe)
					{
						if (pe.SqlState == "23505")
							throw new UniqueIndexViolationException(pe);

						throw;
					}
				}
			}
		}
	}
}
