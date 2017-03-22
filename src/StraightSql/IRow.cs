namespace StraightSql
{
	using System;

	public interface IRow
	{
		T Read<T>(String columnName);
		T ReadEntity<T>(String prefix = null);
	}
}
