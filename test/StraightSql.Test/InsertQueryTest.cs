namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class InsertQueryTest
	{
		[Fact]
		public async Task InsertQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new QueryExecutor(
						new CommandPreparer(),
						new ConnectionFactory(ConnectionString.Default)));

			var queries = new String[]
			{
				"DROP TABLE IF EXISTS insert_query_test;",
				"CREATE TABLE insert_query_test (value TEXT NOT NULL);",
				"INSERT INTO insert_query_test VALUES ('StraightSql');"
			};

			foreach (var query in queries)
				await queryDispatcher.ExecuteAsync(new Query(query));

			var countQuery = "SELECT COUNT(*) FROM insert_query_test;";

			var count = await queryDispatcher.CountAsync(new Query(countQuery));

			Assert.Equal(count, 1);
		}
	}
}
