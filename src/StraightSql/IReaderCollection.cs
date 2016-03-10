namespace StraightSql
{
	public interface IReaderCollection
	{
		T Read<T>(IRow row);
	}
}
