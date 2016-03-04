namespace StraightSql
{
	using Npgsql;
	using System.Threading.Tasks;

	public interface IConnectionFactory
	{
		Task<NpgsqlConnection> CreateAsync();
	}
}
