﻿namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IQueryDispatcher
	{
		Task<Boolean> AnyAsync(IQuery query);
		Task<Int64> CountAsync(IQuery query);
		Task ExecuteAsync(IQuery query);
		Task<T> ExecuteScalarAsync<T>(IQuery query);
		Task<T> FirstAsync<T>(IQuery query, Func<IRow, T> reader);
		Task<T> FirstOrDefaultAsync<T>(IQuery query, Func<IRow, T> reader);
		Task<IList<T>> ListAsync<T>(IQuery query, Func<IRow, T> reader);
		Task<T> SingleAsync<T>(IQuery query, Func<IRow, T> reader);
		Task<T> SingleOrDefaultAsync<T>(IQuery query, Func<IRow, T> reader);
	}
}
