namespace StraightSql
{
	using Npgsql;

	public interface ICommandPreparer
	{
		void Prepare(NpgsqlCommand npgsqlCommand, IQuery query);
	}
}
