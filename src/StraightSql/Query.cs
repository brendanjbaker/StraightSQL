namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;

	public class Query
		: IQuery
	{
		private readonly String text;
		private readonly IEnumerable<NpgsqlParameter> parameters;

		public Query(String text)
			: this(text, new List<NpgsqlParameter>())
		{ }

		public Query(String text, IEnumerable<NpgsqlParameter> parameters)
		{
			if (text == null)
				throw new ArgumentNullException(nameof(text));

			if (parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			this.text = text;
			this.parameters = parameters;
		}

		public IEnumerable<NpgsqlParameter> Parameters => parameters;

		public String Text => text;
	}
}
