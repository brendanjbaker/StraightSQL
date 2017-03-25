namespace StraightSql
{
	using Entity;
	using Npgsql;
	using System;

	public class ContextualizedQueryParameterBuilder
		: IContextualizedQueryParameterBuilder
	{
		private readonly IEntityContext entityContext;
		private readonly IQueryDispatcher queryDispatcher;
		private readonly IQueryParameterBuilder queryParameterBuilder;

		public ContextualizedQueryParameterBuilder(IQueryDispatcher queryDispatcher, IQueryParameterBuilder queryParameterBuilder, IEntityContext entityContext)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (queryParameterBuilder == null)
				throw new ArgumentNullException(nameof(queryParameterBuilder));

			if (entityContext == null)
				throw new ArgumentNullException(nameof(entityContext));

			this.queryDispatcher = queryDispatcher;
			this.queryParameterBuilder = queryParameterBuilder;
			this.entityContext = entityContext;
		}

		public IContextualizedQuery Build()
		{
			var query = queryParameterBuilder.Build();

			return new ContextualizedQuery(query, queryDispatcher, entityContext);
		}

		public IContextualizedQueryParameterBuilder SetLiteral(String name, String value)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));

			queryParameterBuilder.SetLiteral(name, value);

			return this;
		}

		public IContextualizedQueryParameterBuilder SetParameter(NpgsqlParameter npgsqlParameter)
		{
			if (npgsqlParameter == null)
				throw new ArgumentNullException(nameof(npgsqlParameter));

			queryParameterBuilder.SetParameter(npgsqlParameter);

			return this;
		}
	}
}
