namespace StraightSql
{
	using System;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
		public async Task<Int64> CountAsync(IQuery query)
		{
			return await ExecuteQueryAsync(query, async command =>
			{
				var countResult = await command.ExecuteScalarAsync();
				var count = (Int64)countResult;

				return count;
			});
		}
	}
}
