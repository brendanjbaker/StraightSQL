namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class FirstQueryTest
	{
		[Fact]
		public async Task FirstQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new CommandPreparer(),
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS first_query_test;",
				"CREATE TABLE first_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO first_query_test VALUES (1, 'This');",
				"INSERT INTO first_query_test VALUES (2, 'is');",
				"INSERT INTO first_query_test VALUES (3, 'a');",
				"INSERT INTO first_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = @"
				SELECT id, value
				FROM first_query_test
				WHERE id > 2
				ORDER BY id ASC;";

			var item = await queryDispatcher.FirstAsync(new Query(listQuery), row =>
			{
				return new
				{
					id = row.ReadInt32("id"),
					value = row.ReadString("value")
				};
			});

			Assert.NotNull(item);
			Assert.Equal(item.id, 3);
			Assert.Equal(item.value, "a");
		}

		[Fact]
		public async Task FirstQueryThrowsIfEmptyTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new CommandPreparer(),
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS first_query_throws_if_empty_test;",
				"CREATE TABLE first_query_throws_if_empty_test (id INT NOT NULL, value TEXT NOT NULL);"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = @"
				SELECT id, value
				FROM first_query_throws_if_empty_test;";

			await Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				await queryDispatcher.FirstAsync(new Query(listQuery), row =>
				{
					return new
					{
						id = row.ReadInt32("id"),
						value = row.ReadString("value")
					};
				});
			});
		}
	}
}
