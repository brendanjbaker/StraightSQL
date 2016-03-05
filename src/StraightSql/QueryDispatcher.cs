namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Common;
	using System.Threading.Tasks;

	public class QueryDispatcher
		: IQueryDispatcher
	{
		private readonly IConnectionFactory connectionFactory;

		public QueryDispatcher(IConnectionFactory connectionFactory)
		{
			this.connectionFactory = connectionFactory;
		}

		public async Task<Int64> CountAsync(IQuery query)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = query.Text;

					var countResult = await command.ExecuteScalarAsync();
					var count = (Int64)countResult;

					return count;
				}
			}
		}

		public async Task ExecuteAsync(IQuery query)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = query.Text;

					await command.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task<T> FirstAsync<T>(IQuery query, Func<DbDataReader, T> reader)
		{
			var result = await FirstOrDefaultAsync(query, reader);

			if (result == null)
				throw new InvalidOperationException("The sequence contained more than one element.");

			return result;
		}

		public async Task<T> FirstOrDefaultAsync<T>(IQuery query, Func<DbDataReader, T> reader)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = query.Text;

					var dataReader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

					if (!await dataReader.ReadAsync())
						return default(T);

					return reader(dataReader);
				}
			}
		}

		public async Task<IList<T>> ListAsync<T>(IQuery query, Func<DbDataReader, T> readerFunction)
		{
			var list = new List<T>();

			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = query.Text;

					var dataReader = await command.ExecuteReaderAsync();

					while (await dataReader.ReadAsync() != false)
					{
						list.Add(readerFunction(dataReader));
					}
				}
			}

			return list;
		}

		public async Task<T> SingleOrDefaultAsync<T>(IQuery query, Func<DbDataReader, T> reader)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = query.Text;

					var dataReader = await command.ExecuteReaderAsync();

					if (!await dataReader.ReadAsync())
						return default(T);

					var first = reader(dataReader);

					if (await dataReader.ReadAsync())
						return default(T);

					return first;
				}
			}
		}
	}
}
