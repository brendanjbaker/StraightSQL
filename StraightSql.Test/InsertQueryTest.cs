namespace StraightSql.Test
{
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class InsertQueryTest
	{
		[Fact]
		public async Task InsertQueryTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new ConnectionFactory(ConnectionString.Default));

			var queries = new String[]
			{
				"DROP TABLE IF EXISTS insert_query_test;",
				"CREATE TABLE insert_query_test (value TEXT NOT NULL);",
				"INSERT INTO insert_query_test VALUES ('StraightSql');"
			};

			foreach (var query in queries)
				await queryDispatcher.ExecuteAsync(new Query(query));
		}
	}
}
