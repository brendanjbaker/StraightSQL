namespace StraightSql
{
	using Npgsql;
	using System;

	public interface IQueryParameterBuilder
	{
		IQuery Build();
		IQueryParameterBuilder SetLiteral(String name, String value);
		IQueryParameterBuilder SetParameter(NpgsqlParameter npgsqlParameter);
	}
}
