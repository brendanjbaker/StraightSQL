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
		Task<T> FirstOrDefaultAsync<T>();
		Task<IList<T>> ListAsync<T>();
		Task<T> SingleAsync<T>();
		Task<T> SingleOrDefaultAsync<T>();
	}
}
