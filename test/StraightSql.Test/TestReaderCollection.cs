namespace StraightSql.Test
{
	public class TestReaderCollection
	{
		public static readonly ReaderCollection Default;

		static TestReaderCollection()
		{
			Default = new ReaderCollection();
			Default.Add(new TestItemReader());
		}

		private TestReaderCollection()
		{ }
	}
}
