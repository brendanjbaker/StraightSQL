namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;

	public interface IQuery
	{
		IEnumerable<NpgsqlParameter> Parameters { get; }
		String Text { get; }
	}
}
