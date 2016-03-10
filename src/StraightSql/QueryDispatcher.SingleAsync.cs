namespace StraightSql
{
	using System;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
		public async Task<T> SingleAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			var result = await SingleOrDefaultAsync(query, reader);

			if (result == null)
				throw new InvalidOperationException("The sequence did not contain exactly one element.");

			return result;
		}
	}
}
