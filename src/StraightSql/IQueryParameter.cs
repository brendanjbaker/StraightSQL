namespace StraightSql
{
	using System;

	public interface IQueryParameter
	{
		String Name { get; }
		Object Value { get; }
	}
}
