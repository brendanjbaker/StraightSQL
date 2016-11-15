namespace StraightSql
{
	using System;

	public class QueryBuilder
		: IQueryBuilder
	{
		public IQueryIdentifierBuilder SetQuery(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return new QueryIdentifierBuilder(query);
		}
	}
}
