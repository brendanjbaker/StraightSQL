namespace StraightSql
{
	using System;
	using System.Threading.Tasks;

	public interface IQueryDispatcher
	{
		Task<Int64> CountAsync(IQuery query);
		Task ExecuteAsync(IQuery query);
	}
}
