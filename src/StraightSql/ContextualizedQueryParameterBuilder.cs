namespace StraightSql
{
	using System;

	public class ContextualizedQueryParameterBuilder
		: IContextualizedQueryParameterBuilder
	{
		private readonly IQueryDispatcher queryDispatcher;
		private readonly IQueryParameterBuilder queryParameterBuilder;

		public ContextualizedQueryParameterBuilder(IQueryDispatcher queryDispatcher, IQueryParameterBuilder queryParameterBuilder)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (queryParameterBuilder == null)
				throw new ArgumentNullException(nameof(queryParameterBuilder));

			this.queryDispatcher = queryDispatcher;
			this.queryParameterBuilder = queryParameterBuilder;
		}

		public IContextualizedQuery Build()
		{
			var query = queryParameterBuilder.Build();

			return new ContextualizedQuery(query, queryDispatcher);
		}

		public IContextualizedQueryParameterBuilder SetParameter<T>(String name, T value)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));

			queryParameterBuilder.SetParameter(name, value);

			return this;
		}
	}
}
