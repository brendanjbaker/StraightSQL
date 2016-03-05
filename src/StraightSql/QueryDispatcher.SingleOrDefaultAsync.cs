namespace StraightSql
{
	using System;
	using System.Data.Common;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
		public async Task<T> SingleOrDefaultAsync<T>(IQuery query, Func<DbDataReader, T> reader)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					PrepareCommand(command, query);

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
