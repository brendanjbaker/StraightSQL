namespace StraightSql
{
	using System;
	using System.Data.Common;

	public class Row
		: IRow
	{
		private readonly DbDataReader reader;
		private readonly IReaderCollection readerCollection;

		public Row(DbDataReader reader, IReaderCollection readerCollection)
		{
			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			if (readerCollection == null)
				throw new ArgumentNullException(nameof(readerCollection));

			this.reader = reader;
			this.readerCollection = readerCollection;
		}

		public T Read<T>(String columnName)
		{
			if (columnName == null)
				throw new ArgumentNullException(nameof(columnName));

			var value = reader[columnName];

			if (value == DBNull.Value)
				return default(T);

			return (T)value;
		}

		public T ReadEntity<T>(String prefix = null)
		{
			var row = (IRow)this;

			if (prefix != null)
				row = new PrefixedRow(prefix, row);

			return readerCollection.Read<T>(row);
		}
	}
}
