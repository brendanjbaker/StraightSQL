namespace StraightSql
{
	using Entity;
	using System;
	using System.Linq;

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

		public String GetColumnNames<TEntity>(String tablePrefix)
		{
			var fields =
				entityConfigurationCollection
					.Get<TEntity>()
					.Fields
					.Select(f => f.Name)
					.Select(columnName => GetColumnName(tablePrefix, columnName))
					.ToArray();

			return String.Join(", ", fields);
		}

		private static String GetColumnName(String prefix, String columnName)
		{
			if (prefix == null)
				return columnName;

			return String.Format("{0}.{1} AS \"{0}.{1}\"", prefix, columnName);
		}
	}
}
