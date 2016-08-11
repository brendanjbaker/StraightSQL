namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;

	public class Query
		: IQuery
	{
		private readonly UInt32? identifier;
		private readonly String text;
		private readonly IDictionary<String, String> literals;
		private readonly IEnumerable<NpgsqlParameter> parameters;

		public Query(String text)
			: this(text, null)
		{ }

		public Query(String text, UInt32? identifier)
			: this(text, new Dictionary<String, String>(), new List<NpgsqlParameter>(), identifier)
		{ }

		public Query(String text, IDictionary<String, String> literals, IEnumerable<NpgsqlParameter> parameters)
			: this(text, literals, parameters, null)
		{ }

		public Query(String text, IDictionary<String, String> literals, IEnumerable<NpgsqlParameter> parameters, UInt32? identifier)
		{
			if (text == null)
				throw new ArgumentNullException(nameof(text));

			if (literals == null)
				throw new ArgumentNullException(nameof(literals));

			if (parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			this.identifier = identifier;
			this.text = text;
			this.literals = literals;
			this.parameters = parameters;
		}

		public UInt32? Identifier => identifier;

		public IDictionary<String, String> Literals => literals;

		public IEnumerable<NpgsqlParameter> Parameters => parameters;

		public String Text => text;
	}
}
