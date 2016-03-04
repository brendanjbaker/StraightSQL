namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Threading.Tasks;

	public interface IQueryDispatcher
	{
		Task<Int64> CountAsync(IQuery query);
		Task ExecuteAsync(IQuery query);
		Task<IList<T>> ListAsync<T>(IQuery query, Func<DbDataReader, T> reader);
	}
}
