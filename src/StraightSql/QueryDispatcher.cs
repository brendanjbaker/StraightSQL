namespace StraightSql
{
	using Npgsql;

	public partial class QueryDispatcher
		: IQueryDispatcher
	{
		private readonly IConnectionFactory connectionFactory;

		public QueryDispatcher(IConnectionFactory connectionFactory)
		{
			this.connectionFactory = connectionFactory;
		}

		private static void PrepareCommand(NpgsqlCommand npgsqlCommand, IQuery query)
		{
			npgsqlCommand.CommandText = query.Text;

			foreach (var queryParameter in query.Parameters)
			{
				npgsqlCommand.Parameters.Add(queryParameter);
			}
		}
	}
}
