﻿namespace StraightSql
{
	using System;
	using System.Collections.Generic;
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
	}
}
