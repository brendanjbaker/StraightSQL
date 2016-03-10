namespace StraightSql
{
	using System;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
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
	}
}
