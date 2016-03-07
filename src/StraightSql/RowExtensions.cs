namespace StraightSql
{
	using System;

	public static class RowExtensions
	{
		public static Boolean? ReadBoolean(this IRow row, String columnName)
		{
			return row.Read<Boolean?>(columnName);
		}

		public static Byte? ReadByte(this IRow row, String columnName)
		{
			return row.Read<Byte?>(columnName);
		}

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

		public static Int16? ReadInt16(this IRow row, String columnName)
		{
			return row.Read<Int16?>(columnName);
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

		public static UInt16? ReadUInt16(this IRow row, String columnName)
		{
			return row.Read<UInt16?>(columnName);
		}

		public static UInt32? ReadUInt32(this IRow row, String columnName)
		{
			return row.Read<UInt32?>(columnName);
		}

		public static UInt64? ReadUInt64(this IRow row, String columnName)
		{
			return row.Read<UInt64?>(columnName);
		}
	}
}
