namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
		public async Task<IList<T>> ListAsync<T>(IQuery query, Func<IRow, T> readerFunction)
		{
			var list = new List<T>();

			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					PrepareCommand(command, query);

					var dataReader = await command.ExecuteReaderAsync();

					while (await dataReader.ReadAsync() != false)
					{
						list.Add(readerFunction(new Row(dataReader)));
					}
				}
			}

			return list;
		}
	}
}
