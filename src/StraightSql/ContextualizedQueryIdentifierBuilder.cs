namespace StraightSql
{
	using Npgsql;
	using System;

	public class ContextualizedQueryIdentifierBuilder
		: IContextualizedQueryIdentifierBuilder
	{
		private readonly String query;
		private readonly IQueryDispatcher queryDispatcher;
		private readonly IReaderCollection readerCollection;

		public ContextualizedQueryIdentifierBuilder(
			String query,
			IQueryDispatcher queryDispatcher,
			IReaderCollection readerCollection)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (readerCollection == null)
				throw new ArgumentNullException(nameof(readerCollection));

			this.query = query;
			this.queryDispatcher = queryDispatcher;
			this.readerCollection = readerCollection;
		}

		public IContextualizedQuery Build()
		{
			return new ContextualizedQuery(new Query(query), queryDispatcher, readerCollection);
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
			return new ContextualizedQueryParameterBuilder(queryDispatcher, new QueryParameterBuilder(query, identifier), readerCollection);
		}
	}
}
