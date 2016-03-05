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

			var readerCollection = new ReaderCollection();

			readerCollection.Add(new TestItemReader());

			var contextualizedQuery = new ContextualizedQuery(query, queryDispatcher, readerCollection);

			var item = await contextualizedQuery.FirstAsync<TestItem>();

			Assert.NotNull(item);
			Assert.Equal(item.Id, 4);
			Assert.Equal(item.Value, "Delta");
		}
	}
}
