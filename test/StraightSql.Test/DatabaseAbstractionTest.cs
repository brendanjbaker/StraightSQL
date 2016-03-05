namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class DatabaseAbstractionTest
	{
		[Fact]
		public async Task DatabaseAbstractionTestAsync()
		{
			var queryDispatcher = new QueryDispatcher(new ConnectionFactory(ConnectionString.Default));
			var database = new Database(queryDispatcher, TestReaderCollection.Default);

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS database_abstraction_test;",
				"CREATE TABLE database_abstraction_test (id INT NOT NULL, value TEXT NOT NULL);",
				"INSERT INTO database_abstraction_test VALUES (1, 'James');",
				"INSERT INTO database_abstraction_test VALUES (2, 'Madison');",
				"INSERT INTO database_abstraction_test VALUES (3, 'University');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var item =
				await database
					.CreateQuery(@"
						SELECT id, value
						FROM contextualized_query_builder_query_test
						WHERE value = :value;")
					.SetParameter("value", "Hopkins")
					.Build()
					.FirstAsync<TestItem>();

			Assert.NotNull(item);
			Assert.Equal(item.Id, 3);
			Assert.Equal(item.Value, "Hopkins");
		}
	}
}
