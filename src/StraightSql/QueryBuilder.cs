namespace StraightSql
{
	using System;

	public class QueryBuilder
		: IQueryBuilder
	{
		public IQueryParameterBuilder SetQuery(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return new QueryParameterBuilder(query);
		}
	}
}
