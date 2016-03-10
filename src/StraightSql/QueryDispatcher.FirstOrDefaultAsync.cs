namespace StraightSql
{
	using System;
	using System.Data;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
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
	}
}
