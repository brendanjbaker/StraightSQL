namespace StraightSql
{
	using Npgsql;

	public interface IContextualizedQueryParameterBuilder
	{
		IContextualizedQuery Build();
		IContextualizedQueryParameterBuilder SetParameter(NpgsqlParameter npgsqlParameter);
	}
}
