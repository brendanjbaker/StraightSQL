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
			this.parameters = new List<NpgsqlParameter>();
			this.query = query;
		}

		public IQuery Build()
		{
			return new Query(query, parameters);
		}

		public IQueryParameterBuilder SetParameter<T>(String name, T value)
		{
			parameters.Add(new NpgsqlParameter(name, value));

			return this;
		}
	}
}
