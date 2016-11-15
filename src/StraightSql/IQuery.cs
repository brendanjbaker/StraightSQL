namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;

	public interface IQuery
	{
		UInt32? Identifier { get; }
		IDictionary<String, String> Literals { get; }
		IEnumerable<NpgsqlParameter> Parameters { get; }
		String Text { get; }
	}
}
