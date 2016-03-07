namespace StraightSql
{
	using System;
	using System.Data.Common;

	public class Reader
		: IReader
	{
		private readonly Func<DbDataReader, Object> readerFunction;
		private readonly Type type;

		private Reader(Type type, Func<DbDataReader, Object> readerFunction)
		{
			this.type = type;
			this.readerFunction = readerFunction;
		}

		public static Reader Create<T>(IReader<T> reader)
		{
			return new Reader(typeof(T), dataReader =>
			{
				return reader.Read(new Row(dataReader));
			});
		}

		public Type Type
		{
			get { return type; }
		}

		public Object Read(DbDataReader reader)
		{
			return readerFunction(reader);
		}
	}
}
