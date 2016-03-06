namespace StraightSql
{
	using System;
	using System.Data.Common;

	public interface IReader
	{
		Type Type { get; }

		Object Read(DbDataReader reader);
	}
}
