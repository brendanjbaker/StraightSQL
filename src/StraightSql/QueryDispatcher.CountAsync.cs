namespace StraightSql
{
	using System;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
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
	}
}
