namespace StraightSql
{
	using Entity;
	using System;
	using System.Linq;

	public class Database
		: IDatabase
	{
		private readonly IEntityContext entityContext;
		private readonly IQueryDispatcher queryDispatcher;

		public Database(IQueryDispatcher queryDispatcher, IEntityContext entityContext)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (entityContext == null)
				throw new ArgumentNullException(nameof(entityContext));

			this.queryDispatcher = queryDispatcher;
			this.entityContext = entityContext;
		}

		public IContextualizedQueryIdentifierBuilder CreateQuery(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return new ContextualizedQueryIdentifierBuilder(query, queryDispatcher, entityContext);
		}

		public String GetColumnNames<TEntity>(String tablePrefix = null)
		{
			var fields =
				entityContext
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

			return String.Format("\"{0}\".\"{1}\" AS \"{0}.{1}\"", prefix, columnName);
		}
	}
}
