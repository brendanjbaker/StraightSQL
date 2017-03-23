namespace StraightSql
{
	using Entity;
	using System;
	using System.Data.Common;

	public class Row
		: IRow
	{
		private readonly DbDataReader reader;
		private readonly IEntityConfigurationCollection entityConfigurationCollection;

		public Row(DbDataReader reader, IEntityConfigurationCollection entityConfigurationCollection)
		{
			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			if (entityConfigurationCollection == null)
				throw new ArgumentNullException(nameof(entityConfigurationCollection));

			this.reader = reader;
			this.entityConfigurationCollection = entityConfigurationCollection;
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
			where T : new()
		{
			var row = (IRow)this;

			if (prefix != null)
				row = new PrefixedRow(prefix, row);

			return entityConfigurationCollection.Read<T>(row);
		}
	}
}
