namespace StraightSql
{
	using System.Data.Common;

	public interface IReaderCollection
	{
		T Read<T>(DbDataReader dataReader);
	}
}
