namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class SingleOrDefaultQueryTest
	{
		[Fact]
		public async Task SingleOrDefaultQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new CommandPreparer(),
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS single_or_default_query_test;",
				"CREATE TABLE single_or_default_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO single_or_default_query_test VALUES (1, 'This');",
				"INSERT INTO single_or_default_query_test VALUES (2, 'is');",
				"INSERT INTO single_or_default_query_test VALUES (3, 'a');",
				"INSERT INTO single_or_default_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = @"
				SELECT id, value
				FROM single_or_default_query_test
				WHERE id = 4;";

			var item = await queryDispatcher.FirstOrDefaultAsync(new Query(listQuery), row =>
			{
				return new
				{
					id = row.ReadInt32("id"),
					value = row.ReadString("value")
				};
			});

			Assert.NotNull(item);
			Assert.Equal(item.id, 4);
			Assert.Equal(item.value, "test");
		}

		[Fact]
		public async Task SingleOrDefaultIsDefaultQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new CommandPreparer(),
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS single_or_default_is_default_query_test;",
				"CREATE TABLE single_or_default_is_default_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO single_or_default_is_default_query_test VALUES (1, 'This');",
				"INSERT INTO single_or_default_is_default_query_test VALUES (2, 'is');",
				"INSERT INTO single_or_default_is_default_query_test VALUES (3, 'a');",
				"INSERT INTO single_or_default_is_default_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = @"
				SELECT id, value
				FROM single_or_default_is_default_query_test;";

			var item = await queryDispatcher.SingleOrDefaultAsync(new Query(listQuery), row =>
			{
				return new
				{
					id = row.ReadInt32("id"),
					value = row.ReadString("value")
				};
			});

			Assert.Null(item);
		}
	}
}
