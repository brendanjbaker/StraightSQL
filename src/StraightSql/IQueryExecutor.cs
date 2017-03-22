namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Threading.Tasks;

	public interface IQueryExecutor
	{
		Task<T> ExecuteQueryAsync<T>(IQuery query, Func<NpgsqlCommand, Task<T>> functionAsync);
	}
}
