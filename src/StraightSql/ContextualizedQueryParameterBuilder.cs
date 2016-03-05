namespace StraightSql
{
	using System;

	public class ContextualizedQueryParameterBuilder
		: IContextualizedQueryParameterBuilder
	{
		private readonly IQueryDispatcher queryDispatcher;
		private readonly IQueryParameterBuilder queryParameterBuilder;
		private readonly IReaderCollection readerCollection;

		public ContextualizedQueryParameterBuilder(IQueryDispatcher queryDispatcher, IQueryParameterBuilder queryParameterBuilder, IReaderCollection readerCollection)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (queryParameterBuilder == null)
				throw new ArgumentNullException(nameof(queryParameterBuilder));

			if (readerCollection == null)
				throw new ArgumentNullException(nameof(readerCollection));

			this.queryDispatcher = queryDispatcher;
			this.queryParameterBuilder = queryParameterBuilder;
			this.readerCollection = readerCollection;
		}

		public IContextualizedQuery Build()
		{
			var query = queryParameterBuilder.Build();

			return new ContextualizedQuery(query, queryDispatcher, readerCollection);
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
