namespace StraightSql
{
	using System;

	public interface IQueryBuilder
	{
		IQueryParameterBuilder SetQuery(String query);
	}
}
