namespace StraightSql
{
	using System;

	public interface IDatabase
	{
		IContextualizedQueryParameterBuilder CreateQuery(String query);
	}
}
