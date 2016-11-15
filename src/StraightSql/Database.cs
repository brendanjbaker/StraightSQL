namespace StraightSql
{
	using System;

	public class Database
		: IDatabase
	{
		private readonly IQueryDispatcher queryDispatcher;
		private readonly IReaderCollection readerCollection;

		public Database(IQueryDispatcher queryDispatcher, IReaderCollection readerCollection)
		{
			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (readerCollection == null)
				throw new ArgumentNullException(nameof(readerCollection));

			this.queryDispatcher = queryDispatcher;
			this.readerCollection = readerCollection;
		}

		public IContextualizedQueryIdentifierBuilder CreateQuery(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return new ContextualizedQueryIdentifierBuilder(query, queryDispatcher, readerCollection);
		}
	}
}
