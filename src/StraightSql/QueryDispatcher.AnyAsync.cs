namespace StraightSql
{
	using System;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
		public async Task<Boolean> AnyAsync(IQuery query)
		{
			var first = await FirstOrDefaultAsync(query, reader =>
			{
				return new Object();
			});

			return first != null;
		}
	}
}
