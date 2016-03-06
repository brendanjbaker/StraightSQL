namespace StraightSql
{
	using System;
	using System.Data.Common;

	public interface IReader
	{
		Object Read(DbDataReader reader);
		Type Type { get; }
	}
}
