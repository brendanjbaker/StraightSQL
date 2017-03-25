namespace StraightSql
{
	using Entity;
	using Npgsql;
	using System;

	public class ContextualizedQueryParameterBuilder
		: IContextualizedQueryParameterBuilder
	{
		private readonly IEntityContext entityConfigurationCollection;
		private readonly IQueryDispatcher queryDispatcher;
		private readonly IQueryParameterBuilder queryParameterBuilder;

		public ContextualizedQueryParameterBuilder(IQueryDispatcher queryDispatcher, IQueryParameterBuilder queryParameterBuilder, IEntityContext entityConfigurationCollection)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (queryParameterBuilder == null)
				throw new ArgumentNullException(nameof(queryParameterBuilder));

			if (entityConfigurationCollection == null)
				throw new ArgumentNullException(nameof(entityConfigurationCollection));

			this.queryDispatcher = queryDispatcher;
			this.queryParameterBuilder = queryParameterBuilder;
			this.entityConfigurationCollection = entityConfigurationCollection;
		}

		public IContextualizedQuery Build()
		{
			var query = queryParameterBuilder.Build();

			return new ContextualizedQuery(query, queryDispatcher, entityConfigurationCollection);
		}

		public IContextualizedQueryParameterBuilder SetLiteral(String name, String value)
		{
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
