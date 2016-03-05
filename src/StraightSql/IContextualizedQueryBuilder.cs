namespace StraightSql
{
	using System;

	public interface IContextualizedQueryBuilder
	{
		IContextualizedQueryParameterBuilder SetQuery(String query);
	}
}
