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
			this.text = text;
			this.parameters = parameters;
		}

		public IEnumerable<NpgsqlParameter> Parameters
		{
			get { return parameters; }
		}

		public String Text
		{
			get { return text; }
		}
	}
}
