namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class ListQueryTest
	{
		[Fact]
		public async Task ListQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new QueryExecutor(
						new CommandPreparer(),
						new ConnectionFactory(ConnectionString.Default)));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS list_query_test;",
				"CREATE TABLE list_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO list_query_test VALUES (1, 'This');",
				"INSERT INTO list_query_test VALUES (2, 'is');",
				"INSERT INTO list_query_test VALUES (3, 'a');",
				"INSERT INTO list_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = "SELECT id, value FROM list_query_test;";

			var items = await queryDispatcher.ListAsync(new Query(listQuery), row =>
			{
				return new
				{
					id = row.ReadInt32("id"),
					value = row.ReadString("value")
				};
			});

			Assert.NotNull(items);
			Assert.NotEmpty(items);
			Assert.Equal(items.Count, 4);

			Assert.Equal(items[0].id, 1);
			Assert.Equal(items[1].id, 2);
			Assert.Equal(items[2].id, 3);
			Assert.Equal(items[3].id, 4);

			Assert.Equal(items[0].value, "This");
			Assert.Equal(items[1].value, "is");
			Assert.Equal(items[2].value, "a");
			Assert.Equal(items[3].value, "test");
		}

		[Fact]
		public async Task EmptyListQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new QueryExecutor(
						new CommandPreparer(),
						new ConnectionFactory(ConnectionString.Default)));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS empty_list_query_test;",
				"CREATE TABLE empty_list_query_test (id INT NOT NULL, value TEXT NOT NULL);"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = "SELECT id, value FROM empty_list_query_test;";

			var items = await queryDispatcher.ListAsync(new Query(listQuery), row =>
			{
				return new
				{
					id = row.ReadInt32("id"),
					value = row.ReadString("value")
				};
			});

			Assert.NotNull(items);
			Assert.Empty(items);
		}
	}
}
