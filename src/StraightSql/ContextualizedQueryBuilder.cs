namespace StraightSql
{
	using Entity;
	using System;

	public class ContextualizedQueryBuilder
		: IContextualizedQueryBuilder
	{
		private readonly IEntityContext entityContext;
		private readonly IQueryDispatcher queryDispatcher;

		public ContextualizedQueryBuilder(IQueryDispatcher queryDispatcher, IEntityContext entityContext)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (entityContext == null)
				throw new ArgumentNullException(nameof(entityContext));

			this.queryDispatcher = queryDispatcher;
			this.entityContext = entityContext;
		}

		public IContextualizedQueryParameterBuilder SetQuery(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return new ContextualizedQueryParameterBuilder(queryDispatcher, new QueryParameterBuilder(query), entityContext);
		}
	}
}
