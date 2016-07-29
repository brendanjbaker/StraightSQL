namespace StraightSql.Test
{
	using Npgsql;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Xunit;

	public class LiteralQueryTest
	{
		[Fact]
		public async Task LiteralQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new CommandPreparer(),
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS literal_query_test;",
				"CREATE TABLE literal_query_test (id INT NOT NULL);",
				"INSERT INTO literal_query_test VALUES (1);",
				"INSERT INTO literal_query_test VALUES (10);",
				"INSERT INTO literal_query_test VALUES (5);",
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var query = @"
				SELECT id
				FROM literal_query_test
				ORDER BY id :order";

			var literals = new Dictionary<String, String>()
			{
				{ "order", "DESC" }
			};

			var item = await queryDispatcher.FirstAsync(new Query(query, literals, new NpgsqlParameter[0]), row =>
			{
				return new
				{
					id = row.ReadInt32("id")
				};
			});

			Assert.NotNull(item);
			Assert.Equal(item.id, 10);
		}
	}
}
