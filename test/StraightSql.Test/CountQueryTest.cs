namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class CountQueryTest
	{
		[Fact]
		public async Task CountQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new CommandPreparer(),
					new ConnectionFactory(ConnectionString.Default));

			var arrangeQueries = new String[]
			{
				"DROP TABLE IF EXISTS count_query_test;",
				"CREATE TABLE count_query_test (value TEXT NOT NULL);",
				"INSERT INTO count_query_test VALUES ('Sigma');",
				"INSERT INTO count_query_test VALUES ('Nu');",
				"INSERT INTO count_query_test VALUES ('Iota');",
				"INSERT INTO count_query_test VALUES ('Delta');",
				"INSERT INTO count_query_test VALUES ('697');",
			};

			foreach (var arrangeQuery in arrangeQueries)
				await queryDispatcher.ExecuteAsync(new Query(arrangeQuery));

			var countQuery = "SELECT COUNT(*) FROM count_query_test;";

			var count = await queryDispatcher.ExecuteScalarAsync<Int64>(new Query(countQuery));

			Assert.Equal(count, 5);
		}
	}
}
