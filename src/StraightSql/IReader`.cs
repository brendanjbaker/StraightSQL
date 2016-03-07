namespace StraightSql
{
	public interface IReader<T>
	{
		T Read(IRow row);
	}
}
