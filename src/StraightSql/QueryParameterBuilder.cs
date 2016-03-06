namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;

	public class QueryParameterBuilder
		: IQueryParameterBuilder
	{
		private readonly ICollection<NpgsqlParameter> parameters;
		private readonly String query;

		public QueryParameterBuilder(String query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			this.parameters = new List<NpgsqlParameter>();
			this.query = query;
		}

		public IQuery Build()
		{
			return new Query(query, parameters);
		}

		public IQueryParameterBuilder SetParameter(String name, Object value)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));

			parameters.Add(new NpgsqlParameter(name, value ?? DBNull.Value));

			return this;
		}
	}
}
