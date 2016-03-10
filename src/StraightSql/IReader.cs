namespace StraightSql
{
	using System;

	public interface IReader
	{
		Type Type { get; }

		Object Read(IRow row);
	}
}
