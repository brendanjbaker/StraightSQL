namespace StraightSql
{
	using Npgsql;

	public interface IQueryParameterBuilder
	{
		IQuery Build();
		IQueryParameterBuilder SetParameter(NpgsqlParameter npgsqlParameter);
	}
}
