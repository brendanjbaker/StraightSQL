namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class ContextualizedQueryBuilderTest
	{
		[Fact]
		public async Task ContextualizedQueryBuilderTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS contextualized_query_builder_query_test;",
				"CREATE TABLE contextualized_query_builder_query_test (id INT NOT NULL, name TEXT NOT NULL);",
				"INSERT INTO contextualized_query_builder_query_test VALUES (1, 'James');",
				"INSERT INTO contextualized_query_builder_query_test VALUES (2, 'Frank');",
				"INSERT INTO contextualized_query_builder_query_test VALUES (3, 'Hopkins');",
				"INSERT INTO contextualized_query_builder_query_test VALUES (4, 'Greenfield');",
				"INSERT INTO contextualized_query_builder_query_test VALUES (5, 'Quarles');",
				"INSERT INTO contextualized_query_builder_query_test VALUES (6, 'James');",
				"INSERT INTO contextualized_query_builder_query_test VALUES (7, 'McIlvaine');",
				"INSERT INTO contextualized_query_builder_query_test VALUES (8, 'Riley');",
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var contextualizedQueryBuilder = new ContextualizedQueryBuilder(queryDispatcher);

			var item =
				await contextualizedQueryBuilder
					.SetQuery(@"
						SELECT id, name
						FROM contextualized_query_builder_query_test
						WHERE name = :name;")
					.SetParameter("name", "Hopkins")
					.Build()
					.FirstAsync(reader =>
					{
						return new
						{
							id = (Int32)reader["id"],
							name = (String)reader["name"]
						};
					});

			Assert.NotNull(item);
			Assert.Equal(item.id, 3);
			Assert.Equal(item.name, "Hopkins");
		}
	}
}
