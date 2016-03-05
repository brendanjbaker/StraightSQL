namespace StraightSql
{
	using System;
	using System.Data;
	using System.Data.Common;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
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
	}
}
