namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ReaderCollection
		: IReaderCollection
	{
		private readonly IEnumerable<IReader> readers;

		public ReaderCollection(IEnumerable<IReader> readers)
		{
			if (readers == null)
				throw new ArgumentNullException(nameof(readers));

			this.readers = readers;
		}

		public T Read<T>(IRow row)
		{
			if (row == null)
				throw new ArgumentNullException(nameof(row));

			var reader = readers.SingleOrDefault(r => r.Type == typeof(T));

			if (reader == null)
				throw new ReaderNotFoundException(typeof(T));

			var instance = reader.Read(row);

			if (instance.GetType() != typeof(T))
				throw new ReaderTypeMismatchException(typeof(T), instance.GetType());

			return (T)instance;
		}
	}
}
