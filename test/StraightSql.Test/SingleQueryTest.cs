namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class SingleQueryTest
	{
		[Fact]
		public async Task SingleQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS single_query_test;",
				"CREATE TABLE single_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO single_query_test VALUES (1, 'This');",
				"INSERT INTO single_query_test VALUES (2, 'is');",
				"INSERT INTO single_query_test VALUES (3, 'a');",
				"INSERT INTO single_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = @"
				SELECT id, value
				FROM single_query_test
				WHERE id = 2;";

			var item = await queryDispatcher.SingleAsync(new Query(listQuery), reader =>
			{
				return new
				{
					id = (Int32)reader["id"],
					value = (String)reader["value"]
				};
			});

			Assert.NotNull(item);
			Assert.Equal(item.id, 2);
			Assert.Equal(item.value, "is");
		}

		[Fact]
		public async Task SingleQueryThrowsIfEmptyTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS single_query_throws_if_empty_test;",
				"CREATE TABLE single_query_throws_if_empty_test (id INT NOT NULL, value TEXT NOT NULL);"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = @"
				SELECT id, value
				FROM single_query_throws_if_empty_test;";

			await Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				await queryDispatcher.SingleAsync(new Query(listQuery), reader =>
				{
					return new
					{
						id = (Int32)reader["id"],
						value = (String)reader["value"]
					};
				});
			});
		}

		[Fact]
		public async Task SingleQueryThrowsIfMultipleTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS single_query_throws_if_multiple_test;",
				"CREATE TABLE single_query_throws_if_multiple_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO single_query_throws_if_multiple_test VALUES (1, 'This');",
				"INSERT INTO single_query_throws_if_multiple_test VALUES (2, 'is');",
				"INSERT INTO single_query_throws_if_multiple_test VALUES (3, 'a');",
				"INSERT INTO single_query_throws_if_multiple_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = @"
				SELECT id, value
				FROM single_query_throws_if_multiple_test;";

			await Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				await queryDispatcher.SingleAsync(new Query(listQuery), reader =>
				{
					return new
					{
						id = (Int32)reader["id"],
						value = (String)reader["value"]
					};
				});
			});
		}
	}
}
