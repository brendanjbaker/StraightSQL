namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
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
