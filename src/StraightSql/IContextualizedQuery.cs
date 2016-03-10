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
		Task<T> FirstAsync<T>();
		Task<T> FirstAsync<T>(Func<IRow, T> reader);
		Task<T> FirstOrDefaultAsync<T>();
		Task<T> FirstOrDefaultAsync<T>(Func<IRow, T> reader);
		Task<IList<T>> ListAsync<T>();
		Task<IList<T>> ListAsync<T>(Func<IRow, T> reader);
		Task<T> SingleAsync<T>();
		Task<T> SingleAsync<T>(Func<IRow, T> reader);
		Task<T> SingleOrDefaultAsync<T>();
		Task<T> SingleOrDefaultAsync<T>(Func<IRow, T> reader);
	}
}
