namespace StraightSql
{
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Linq;

	public class ReaderCollection
		: IReaderCollection
	{
		private readonly IEnumerable<IReader> readers;

		public ReaderCollection(IEnumerable<IReader> readers)
		{
			this.readers = readers;
		}

		public T Read<T>(DbDataReader dataReader)
		{
			var reader = readers.SingleOrDefault(r => r.Type == typeof(T));

			if (reader == null)
				throw new ReaderNotFoundException(typeof(T));

			return (T)reader.Read(dataReader);
		}
	}
}
