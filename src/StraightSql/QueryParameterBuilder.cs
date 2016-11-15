namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;

	public class QueryParameterBuilder
		: IQueryParameterBuilder
	{
		private readonly UInt32? identifier;
		private readonly IDictionary<String, String> literals;
		private readonly ICollection<NpgsqlParameter> parameters;
		private readonly String query;

		public QueryParameterBuilder(String query)
			: this(query, null)
		{ }

		public QueryParameterBuilder(String query, UInt32? identifier)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			this.identifier = identifier;
			this.literals = new Dictionary<String, String>();
			this.parameters = new List<NpgsqlParameter>();
			this.query = query;
		}

		public IQuery Build()
		{
			return new Query(query, literals, parameters, identifier);
		}

		public IQueryParameterBuilder SetLiteral(String name, String value)
		{
			if (String.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			if (value == null)
				throw new ArgumentNullException(nameof(value));

			literals.Add(new KeyValuePair<String, String>(name, value));

			return this;
		}

		public IQueryParameterBuilder SetParameter(NpgsqlParameter npgsqlParameter)
		{
			if (npgsqlParameter == null)
				throw new ArgumentNullException(nameof(npgsqlParameter));

			parameters.Add(npgsqlParameter);

			return this;
		}
	}
}
