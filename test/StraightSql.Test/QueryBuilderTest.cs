namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class QueryBuilderTest
	{
		[Fact]
		public async Task QueryBuilderTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS query_builder_query_test;",
				"CREATE TABLE query_builder_query_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO query_builder_query_test VALUES (1, 'This');",
				"INSERT INTO query_builder_query_test VALUES (2, 'is');",
				"INSERT INTO query_builder_query_test VALUES (3, 'a');",
				"INSERT INTO query_builder_query_test VALUES (4, 'test');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var query =
				new QueryBuilder()
					.SetQuery(@"
						SELECT id, value
						FROM query_builder_query_test
						WHERE id > :id
						ORDER BY id ASC;")
					.SetParameter("id", 3)
					.Build();

			var item = await queryDispatcher.FirstAsync(query, row =>
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
	}
}
