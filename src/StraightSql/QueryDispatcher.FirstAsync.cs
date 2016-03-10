﻿namespace StraightSql
{
	using System;
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
		public async Task<T> FirstAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			var result = await FirstOrDefaultAsync(query, reader);

			if (result == null)
				throw new InvalidOperationException("The sequence contained no elements.");

			return result;
		}
	}
}
