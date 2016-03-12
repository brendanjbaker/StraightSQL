namespace StraightSql
{
	using System;
	using System.Data.Common;

	public class Row
		: IRow
	{
		private readonly DbDataReader reader;

		public Row(DbDataReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			this.reader = reader;
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
	}
}
