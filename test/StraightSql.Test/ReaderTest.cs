namespace StraightSql.Test
{
	using System;
	using System.Data.Common;
	using System.Threading.Tasks;
	using Xunit;

	public class ReaderTest
	{
		[Fact]
		public async Task ReaderTestAsync()
		{
			var queryDispatcher =
				new QueryDispatcher(
					new ConnectionFactory(ConnectionString.Default));

			var setupQueries = new String[]
			{
				"DROP TABLE IF EXISTS reader_test;",
				"CREATE TABLE reader_test (id INT NOT NULL, first_name TEXT NOT NULL, middle_name TEXT, last_name TEXT NOT NULL);",
				"INSERT INTO reader_test VALUES (417, 'Brendan', 'James', 'Baker');"
			};

			foreach (var setupQuery in setupQueries)
				await queryDispatcher.ExecuteAsync(new Query(setupQuery));

			var readerCollection = new ReaderCollection();

			readerCollection.Add(new PersonReader());

			var contextualizedQueryBuilder = new ContextualizedQueryBuilder(queryDispatcher, readerCollection);

			var item =
				await contextualizedQueryBuilder
					.SetQuery(@"
						SELECT id, first_name, middle_name, last_name
						FROM reader_test
						WHERE last_name = :last_name;")
					.SetParameter("last_name", "Baker")
					.Build()
					.SingleAsync<Person>();

			Assert.NotNull(item);
			Assert.Equal(item.Id, 417);
			Assert.Equal(item.FirstName, "Brendan");
			Assert.Equal(item.MiddleName, "James");
			Assert.Equal(item.LastName, "Baker");
		}

		private class Person
		{
			public String FirstName { get; set; }
			public Int32 Id { get; set; }
			public String LastName { get; set; }
			public String MiddleName { get; set; }
		}

		private class PersonReader
			: IReader<Person>
		{
			public Person Read(DbDataReader reader)
			{
				return new Person()
				{
					FirstName = (String)reader["first_name"],
					Id = (Int32)reader["id"],
					LastName = (String)reader["last_name"],
					MiddleName = (String)reader["middle_name"],
				};
			}
		}
	}
}
