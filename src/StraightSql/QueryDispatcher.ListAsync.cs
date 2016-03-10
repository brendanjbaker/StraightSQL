namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
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
	}
}
