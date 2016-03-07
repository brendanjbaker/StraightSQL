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
			this.reader = reader;
		}

		public T Read<T>(String columnName)
		{
			var value = reader[columnName];

			if (value == DBNull.Value)
				return default(T);

			return (T)value;
		}
	}
}
