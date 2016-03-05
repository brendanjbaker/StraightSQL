﻿namespace StraightSql.Test
{
	using Npgsql;
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class ParameterizedQueryTest
	{
		[Fact]
		public async Task ParameterizedQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS parameterized_query_test;",
				"CREATE TABLE parameterized_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO parameterized_query_test VALUES (1, 'This');",
				"INSERT INTO parameterized_query_test VALUES (2, 'is');",
				"INSERT INTO parameterized_query_test VALUES (3, 'a');",
				"INSERT INTO parameterized_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var query = @"
				SELECT id, value
				FROM parameterized_query_test
				WHERE id > :id
				ORDER BY id ASC;";

			var parameters = new NpgsqlParameter[]
			{
				new NpgsqlParameter("id", 1)
			};

			var item = await queryDispatcher.FirstAsync(new Query(query, parameters), reader =>
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
	}
}