namespace StraightSql
{
	using Npgsql;
	using System;

	public class QueryIdentifierBuilder
		: IQueryIdentifierBuilder
	{
		private readonly String query;

		public QueryIdentifierBuilder(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			this.query = query;
		}

		public IQuery Build()
		{
			return new Query(query);
		}

		public IQueryParameterBuilder SetIdentifier(UInt32 identifier)
		{
			return new QueryParameterBuilder(query, identifier);
		}

		public IQueryParameterBuilder SetLiteral(String name, String value)
		{
			return new QueryParameterBuilder(query).SetLiteral(name, value);
		}

		public IQueryParameterBuilder SetParameter(NpgsqlParameter npgsqlParameter)
		{
			return new QueryParameterBuilder(query).SetParameter(npgsqlParameter);
		}
	}
}
