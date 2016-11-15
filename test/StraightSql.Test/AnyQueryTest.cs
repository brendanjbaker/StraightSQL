namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class AnyQueryTest
	{
		[Fact]
		public async Task AnyQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new QueryExecutor(
						new CommandPreparer(),
						new ConnectionFactory(ConnectionString.Default)));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS any_query_test;",
				"CREATE TABLE any_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO any_query_test VALUES (1, 'This');",
				"INSERT INTO any_query_test VALUES (2, 'is');",
				"INSERT INTO any_query_test VALUES (3, 'a');",
				"INSERT INTO any_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = "SELECT id, value FROM any_query_test;";

			var isAny = await queryDispatcher.AnyAsync(new Query(listQuery));

			Assert.True(isAny);
		}

		[Fact]
		public async Task AnyEmptyQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new QueryExecutor(
						new CommandPreparer(),
						new ConnectionFactory(ConnectionString.Default)));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS any_query_empty_test;",
				"CREATE TABLE any_query_empty_test (id INT NOT NULL, value TEXT NOT NULL);"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = "SELECT id, value FROM any_query_empty_test;";

			var isAny = await queryDispatcher.AnyAsync(new Query(listQuery));

			Assert.False(isAny);
		}
	}
}
