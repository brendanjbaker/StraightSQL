namespace StraightSql
{
	using System.Data.Common;

	public interface IReader<T>
	{
		T Read(DbDataReader reader);
	}
}
