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
			this.queryDispatcher = queryDispatcher;
			this.readerCollection = readerCollection;
		}

		public IContextualizedQueryParameterBuilder CreateQuery(String query)
		{
			return new ContextualizedQueryParameterBuilder(queryDispatcher, new QueryParameterBuilder(query), readerCollection);
		}
	}
}
