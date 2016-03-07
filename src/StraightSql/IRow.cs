namespace StraightSql
{
	using System;

	public interface IRow
	{
		T Read<T>(String columnName);
	}
}
