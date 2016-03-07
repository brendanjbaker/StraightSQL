namespace StraightSql
{
	using System;

	public static class RowExtensions
	{
		public static Byte[] ReadByteArray(this IRow row, String columnName)
		{
			return row.Read<Byte[]>(columnName);
		}

		public static DateTime? ReadDateTime(this IRow row, String columnName)
		{
			return row.Read<DateTime?>(columnName);
		}

		public static Guid? ReadGuid(this IRow row, String columnName)
		{
			return row.Read<Guid?>(columnName);
		}

		public static Int32? ReadInt32(this IRow row, String columnName)
		{
			return row.Read<Int32?>(columnName);
		}

		public static Int64? ReadInt64(this IRow row, String columnName)
		{
			return row.Read<Int64?>(columnName);
		}

		public static String ReadString(this IRow row, String columnName)
		{
			return row.Read<String>(columnName);
		}
	}
}
