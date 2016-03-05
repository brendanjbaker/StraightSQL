namespace StraightSql
{
	using Npgsql;
	using System;

	public partial class QueryDispatcher
		: IQueryDispatcher
	{
		private readonly IConnectionFactory connectionFactory;

		public QueryDispatcher(IConnectionFactory connectionFactory)
		{
			if (connectionFactory == null)
				throw new ArgumentNullException(nameof(connectionFactory));

			this.connectionFactory = connectionFactory;
		}

		private static void PrepareCommand(NpgsqlCommand npgsqlCommand, IQuery query)
		{
			if (npgsqlCommand == null)
				throw new ArgumentNullException(nameof(npgsqlCommand));

			if (query == null)
				throw new ArgumentNullException(nameof(query));

			npgsqlCommand.CommandText = query.Text;

			foreach (var queryParameter in query.Parameters)
			{
				npgsqlCommand.Parameters.Add(queryParameter);
			}
		}
	}
}
