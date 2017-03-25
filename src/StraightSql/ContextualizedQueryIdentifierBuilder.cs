namespace StraightSql
{
	using Entity;
	using Npgsql;
	using System;

	public class ContextualizedQueryIdentifierBuilder
		: IContextualizedQueryIdentifierBuilder
	{
		private readonly IEntityContext entityContext;
		private readonly String query;
		private readonly IQueryDispatcher queryDispatcher;

		public ContextualizedQueryIdentifierBuilder(
			String query,
			IQueryDispatcher queryDispatcher,
			IEntityContext entityContext)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (entityContext == null)
				throw new ArgumentNullException(nameof(entityContext));

			this.query = query;
			this.queryDispatcher = queryDispatcher;
			this.entityContext = entityContext;
		}

		public IContextualizedQuery Build()
		{
			return new ContextualizedQuery(new Query(query), queryDispatcher, entityContext);
		}

		public IContextualizedQueryParameterBuilder SetIdentifier(UInt32 identifier)
		{
			return CreateNextBuilder(identifier);
		}

		public IContextualizedQueryParameterBuilder SetLiteral(String name, String value)
		{
			return CreateNextBuilder().SetLiteral(name, value);
		}

		public IContextualizedQueryParameterBuilder SetParameter(NpgsqlParameter npgsqlParameter)
		{
			return CreateNextBuilder().SetParameter(npgsqlParameter);
		}

		private IContextualizedQueryParameterBuilder CreateNextBuilder(UInt32? identifier = null)
		{
			return new ContextualizedQueryParameterBuilder(queryDispatcher, new QueryParameterBuilder(query, identifier), entityContext);
		}
	}
}
