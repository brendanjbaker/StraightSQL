namespace StraightSql
{
	using System;

	public class ContextualizedQueryBuilder
		: IContextualizedQueryBuilder
	{
		private readonly IQueryDispatcher queryDispatcher;

		public ContextualizedQueryBuilder(IQueryDispatcher queryDispatcher)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			this.queryDispatcher = queryDispatcher;
		}

		public IContextualizedQueryParameterBuilder SetQuery(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return new ContextualizedQueryParameterBuilder(queryDispatcher, new QueryParameterBuilder(query));
		}
	}
}
