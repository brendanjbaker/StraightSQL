namespace StraightSql
{
	using System;

	public class QueryBuilder
		: IQueryBuilder
	{
		public IQueryParameterBuilder SetQuery(String query)
		{
			return new QueryParameterBuilder(query);
		}
	}
}
