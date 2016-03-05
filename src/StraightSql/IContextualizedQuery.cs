namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Threading.Tasks;

	public interface IContextualizedQuery
		: IQuery
	{
		Task<Boolean> AnyAsync();
		Task<Int64> CountAsync();
		Task ExecuteAsync();
		Task<T> FirstAsync<T>(Func<DbDataReader, T> reader);
		Task<T> FirstOrDefaultAsync<T>(Func<DbDataReader, T> reader);
		Task<IList<T>> ListAsync<T>(Func<DbDataReader, T> reader);
		Task<T> SingleAsync<T>(Func<DbDataReader, T> reader);
		Task<T> SingleOrDefaultAsync<T>(Func<DbDataReader, T> reader);
	}
}
