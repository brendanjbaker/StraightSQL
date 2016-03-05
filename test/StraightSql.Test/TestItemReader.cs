namespace StraightSql.Test
{
	using System;
	using System.Data.Common;

	public class TestItemReader
		: IReader<TestItem>
	{
		public TestItem Read(DbDataReader reader)
		{
			return new TestItem()
			{
				Id = (Int32)reader["id"],
				Value = (String)reader["value"]
			};
		}
	}
}
