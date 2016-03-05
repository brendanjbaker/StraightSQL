namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class ContextualizedQueryTest
	{
		[Fact]
		public async Task ContextualizedQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS contextualized_query_test;",
				"CREATE TABLE contextualized_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO contextualized_query_test VALUES (1, 'Sigma');",
				"INSERT INTO contextualized_query_test VALUES (2, 'Nu');",
				"INSERT INTO contextualized_query_test VALUES (3, 'Iota');",
				"INSERT INTO contextualized_query_test VALUES (4, 'Delta');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var query =
				new QueryBuilder()
					.SetQuery(@"
						SELECT id, value
						FROM contextualized_query_test
						WHERE value = :greekLetter;")
					.SetParameter("greekLetter", "Delta")
					.Build();

			var contextualizedQuery = new ContextualizedQuery(query, queryDispatcher);

			var item = await contextualizedQuery.FirstAsync(reader =>
			{
				return new
				{
					id = (Int32)reader["id"],
					value = (String)reader["value"]
				};
			});

			Assert.NotNull(item);
			Assert.Equal(item.id, 4);
			Assert.Equal(item.value, "Delta");
		}
	}
}
