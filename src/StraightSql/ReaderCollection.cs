namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;

	public class ReaderCollection
		: IReaderCollection
	{
		private readonly IDictionary<Type, Func<DbDataReader, Object>> readerFunctions;

		public ReaderCollection()
		{
			this.readerFunctions = new Dictionary<Type, Func<DbDataReader, Object>>();
		}

		public void Add<T>(IReader<T> reader)
		{
			readerFunctions.Add(typeof(T), dbDataReader =>
			{
				return reader.Read(dbDataReader);
			});
		}

		public T Read<T>(DbDataReader reader)
		{
			Func<DbDataReader, Object> readerFunction;

			if (!readerFunctions.TryGetValue(typeof(T), out readerFunction))
				throw new ReaderNotFoundException(typeof(T));

			return (T)readerFunction(reader);
		}
	}
}
