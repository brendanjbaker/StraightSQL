namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IContextualizedQuery
		: IQuery
	{
		Task<Boolean> AnyAsync();
		Task<Int64> CountAsync();
		Task ExecuteAsync();
		Task<T> ExecuteScalarAsync<T>();
		Task<T> FirstAsync<T>() where T : new();
		Task<T> FirstAsync<T>(Func<IRow, T> reader);
		Task<T> FirstOrDefaultAsync<T>() where T : new();
		Task<T> FirstOrDefaultAsync<T>(Func<IRow, T> reader);
		Task<IList<T>> ListAsync<T>() where T : new();
		Task<IList<T>> ListAsync<T>(Func<IRow, T> reader);
		Task<T> SingleAsync<T>() where T : new();
		Task<T> SingleAsync<T>(Func<IRow, T> reader);
		Task<T> SingleOrDefaultAsync<T>() where T : new();
		Task<T> SingleOrDefaultAsync<T>(Func<IRow, T> reader);
	}
}
