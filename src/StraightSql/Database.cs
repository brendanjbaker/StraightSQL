namespace StraightSql
{
	using Entity;
	using System;

	public class Database
		: IDatabase
	{
		private readonly IEntityConfigurationCollection entityConfigurationCollection;
		private readonly IQueryDispatcher queryDispatcher;

		public Database(IQueryDispatcher queryDispatcher, IEntityConfigurationCollection entityConfigurationCollection)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (entityConfigurationCollection == null)
				throw new ArgumentNullException(nameof(entityConfigurationCollection));

			this.queryDispatcher = queryDispatcher;
			this.entityConfigurationCollection = entityConfigurationCollection;
		}

		public IContextualizedQueryIdentifierBuilder CreateQuery(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return new ContextualizedQueryIdentifierBuilder(query, queryDispatcher, entityConfigurationCollection);
		}
	}
}
