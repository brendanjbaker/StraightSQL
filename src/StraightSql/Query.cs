namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;

	public class Query
		: IQuery
	{
		private readonly String text;
		private readonly IDictionary<String, String> literals;
		private readonly IEnumerable<NpgsqlParameter> parameters;

		public Query(String text)
			: this(text, new Dictionary<String, String>(), new List<NpgsqlParameter>())
		{ }

		public Query(String text, IDictionary<String, String> literals, IEnumerable<NpgsqlParameter> parameters)
		{
			if (text == null)
				throw new ArgumentNullException(nameof(text));

			if (literals == null)
				throw new ArgumentNullException(nameof(literals));

			if (parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			this.text = text;
			this.literals = literals;
			this.parameters = parameters;
		}

		public IDictionary<String, String> Literals => literals;

		public IEnumerable<NpgsqlParameter> Parameters => parameters;

		public String Text => text;
	}
}
