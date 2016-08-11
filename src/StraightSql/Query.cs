﻿namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;

	public class Query
		: IQuery
	{
		private readonly UInt32? id;
		private readonly String text;
		private readonly IDictionary<String, String> literals;
		private readonly IEnumerable<NpgsqlParameter> parameters;

		public Query(String text)
			: this(text, null)
		{ }

		public Query(String text, UInt32? id)
			: this(text, new Dictionary<String, String>(), new List<NpgsqlParameter>(), id)
		{ }

		public Query(String text, IDictionary<String, String> literals, IEnumerable<NpgsqlParameter> parameters)
			: this(text, literals, parameters, null)
		{ }

		public Query(String text, IDictionary<String, String> literals, IEnumerable<NpgsqlParameter> parameters, UInt32? id)
		{
			if (text == null)
				throw new ArgumentNullException(nameof(text));

			if (literals == null)
				throw new ArgumentNullException(nameof(literals));

			if (parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			this.id = id;
			this.text = text;
			this.literals = literals;
			this.parameters = parameters;
		}

		public UInt32? Id => id;

		public IDictionary<String, String> Literals => literals;

		public IEnumerable<NpgsqlParameter> Parameters => parameters;

		public String Text => text;
	}
}
