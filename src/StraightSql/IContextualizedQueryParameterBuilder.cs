namespace StraightSql
{
	using Npgsql;
	using System;

	public interface IContextualizedQueryParameterBuilder
	{
		IContextualizedQuery Build();
		IContextualizedQueryParameterBuilder SetLiteral(String name, String value);
		IContextualizedQueryParameterBuilder SetParameter(NpgsqlParameter npgsqlParameter);
	}
}
