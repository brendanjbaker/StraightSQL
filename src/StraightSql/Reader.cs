namespace StraightSql
{
	using System;

	public class Reader
		: IReader
	{
		private readonly Func<IRow, Object> readerFunction;
		private readonly Type type;

		private Reader(Type type, Func<IRow, Object> readerFunction)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			if (readerFunction == null)
				throw new ArgumentNullException(nameof(readerFunction));

			this.type = type;
			this.readerFunction = readerFunction;
		}

		public static Reader Create<T>(IReader<T> reader)
		{
			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			return new Reader(typeof(T), row =>
			{
				return reader.Read(row);
			});
		}

		public Type Type
		{
			get { return type; }
		}

		public Object Read(IRow row)
		{
			return readerFunction(row);
		}
	}
}
