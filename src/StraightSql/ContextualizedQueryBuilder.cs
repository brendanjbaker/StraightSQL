namespace StraightSql
{
	using Entity;
	using System;

	public class ContextualizedQueryBuilder
		: IContextualizedQueryBuilder
	{
		private readonly IEntityContext entityConfigurationCollection;
		private readonly IQueryDispatcher queryDispatcher;

		public ContextualizedQueryBuilder(IQueryDispatcher queryDispatcher, IEntityContext entityConfigurationCollection)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (entityConfigurationCollection == null)
				throw new ArgumentNullException(nameof(entityConfigurationCollection));

			this.queryDispatcher = queryDispatcher;
			this.entityConfigurationCollection = entityConfigurationCollection;
		}

		public IContextualizedQueryParameterBuilder SetQuery(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return new ContextualizedQueryParameterBuilder(queryDispatcher, new QueryParameterBuilder(query), entityConfigurationCollection);
		}
	}
}
