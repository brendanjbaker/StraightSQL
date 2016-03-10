namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Threading.Tasks;

	public class QueryDispatcher
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

		public async Task<Boolean> AnyAsync(IQuery query)
		{
			var first = await FirstOrDefaultAsync(query, reader =>
			{
				return new Object();
			});

			return first != null;
		}

		public async Task<Int64> CountAsync(IQuery query)
		{
			return await ExecuteQueryAsync(query, async command =>
			{
				var countResult = await command.ExecuteScalarAsync();
				var count = (Int64)countResult;

				return count;
			});
		}

		public async Task ExecuteAsync(IQuery query)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					commandPreparer.Prepare(command, query);

					await command.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task<T> FirstAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			var result = await FirstOrDefaultAsync(query, reader);

			if (result == null)
				throw new InvalidOperationException("The sequence contained no elements.");

			return result;
		}

		public async Task<T> FirstOrDefaultAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					commandPreparer.Prepare(command, query);

					var dataReader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

					if (!await dataReader.ReadAsync())
						return default(T);

					return reader(new Row(dataReader));
				}
			}
		}

		public async Task<IList<T>> ListAsync<T>(IQuery query, Func<IRow, T> readerFunction)
		{
			return await ExecuteQueryAsync(query, async command =>
			{
				var list = new List<T>();
				var dataReader = await command.ExecuteReaderAsync();

				while (await dataReader.ReadAsync() != false)
				{
					list.Add(readerFunction(new Row(dataReader)));
				}

				return list;
			});
		}

		public async Task<T> SingleAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			var result = await SingleOrDefaultAsync(query, reader);

			if (result == null)
				throw new InvalidOperationException("The sequence did not contain exactly one element.");

			return result;
		}

		public async Task<T> SingleOrDefaultAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			return await ExecuteQueryAsync(query, async command =>
			{
				var dataReader = await command.ExecuteReaderAsync();

				if (!await dataReader.ReadAsync())
					return default(T);

				var first = reader(new Row(dataReader));

				if (await dataReader.ReadAsync())
					return default(T);

				return first;
			});
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
