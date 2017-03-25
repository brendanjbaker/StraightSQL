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
			if (prefix == null)
				throw new ArgumentNullException(nameof(prefix));

			if (row == null)
				throw new ArgumentNullException(nameof(row));

			this.prefix = prefix;
			this.row = row;
		}

		public T Read<T>(String columnName)
		{
			if (columnName == null)
				throw new ArgumentNullException(nameof(columnName));

			return row.Read<T>($"{prefix}.{columnName}");
		}

		public T ReadEntity<T>(String prefix = null)
			where T : new()
		{
			return row.ReadEntity<T>(prefix);
		}
	}
}
