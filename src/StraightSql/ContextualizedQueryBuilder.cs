namespace StraightSql
{
	using System;

	public class ContextualizedQueryBuilder
		: IContextualizedQueryBuilder
	{
		private readonly IQueryDispatcher queryDispatcher;
		private readonly IReaderCollection readerCollection;

		public ContextualizedQueryBuilder(IQueryDispatcher queryDispatcher, IReaderCollection readerCollection)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (readerCollection == null)
				throw new ArgumentNullException(nameof(readerCollection));

			this.queryDispatcher = queryDispatcher;
			this.readerCollection = readerCollection;
		}

		public IContextualizedQueryParameterBuilder SetQuery(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return new ContextualizedQueryParameterBuilder(queryDispatcher, new QueryParameterBuilder(query), readerCollection);
		}
	}
}
