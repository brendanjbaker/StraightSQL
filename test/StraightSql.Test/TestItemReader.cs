namespace StraightSql.Test
{
	public class TestItemReader
		: IReader<TestItem>
	{
		public TestItem Read(IRow row)
		{
			return new TestItem()
			{
				Id = row.ReadInt32("id").Value,
				Value = row.ReadString("value")
			};
		}
	}
}
