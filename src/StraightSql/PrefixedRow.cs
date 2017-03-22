namespace StraightSql
{
	using System;

	public class PrefixedRow
		: IRow
	{
		private readonly String prefix;
		private readonly IRow row;

		public PrefixedRow(String prefix, IRow row)
		{
			this.prefix = prefix;
			this.row = row;
		}

		public T Read<T>(String columnName)
		{
			return row.Read<T>($"{prefix}.{columnName}");
		}

		public T ReadEntity<T>(String prefix = null)
		{
			return row.ReadEntity<T>(prefix);
		}
	}
}
