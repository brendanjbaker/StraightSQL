﻿namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class FirstOrDefaultQueryTest
	{
		[Fact]
		public async Task FirstOrDefaultQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new QueryExecutor(
						new CommandPreparer(),
						new ConnectionFactory(ConnectionString.Default)));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS first_or_default_query_test;",
				"CREATE TABLE first_or_default_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO first_or_default_query_test VALUES (1, 'This');",
				"INSERT INTO first_or_default_query_test VALUES (2, 'is');",
				"INSERT INTO first_or_default_query_test VALUES (3, 'a');",
				"INSERT INTO first_or_default_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = @"
				SELECT id, value
				FROM first_or_default_query_test
				WHERE id > 2
				ORDER BY id ASC;";

			var item = await queryDispatcher.FirstOrDefaultAsync(new Query(listQuery), row =>
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
		public async Task FirstOrDefaultIsDefaultQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new QueryExecutor(
						new CommandPreparer(),
						new ConnectionFactory(ConnectionString.Default)));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS first_or_default_is_default_query_test;",
				"CREATE TABLE first_or_default_is_default_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO first_or_default_is_default_query_test VALUES (1, 'This');",
				"INSERT INTO first_or_default_is_default_query_test VALUES (2, 'is');",
				"INSERT INTO first_or_default_is_default_query_test VALUES (3, 'a');",
				"INSERT INTO first_or_default_is_default_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var listQuery = @"
				SELECT id, value
				FROM first_or_default_is_default_query_test
				WHERE id > 4
				ORDER BY id ASC;";

			var item = await queryDispatcher.FirstOrDefaultAsync(new Query(listQuery), row =>
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
