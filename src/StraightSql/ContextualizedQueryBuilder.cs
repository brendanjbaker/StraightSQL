namespace StraightSql
{
	using System;

	public class ContextualizedQueryBuilder
		: IContextualizedQueryBuilder
	{
		private readonly IQueryDispatcher queryDispatcher;

		public ContextualizedQueryBuilder(IQueryDispatcher queryDispatcher)
		{
			this.queryDispatcher = queryDispatcher;
		}

		public IContextualizedQueryParameterBuilder SetQuery(String query)
		{
			return new ContextualizedQueryParameterBuilder(queryDispatcher, new QueryParameterBuilder(query));
		}
	}
}
