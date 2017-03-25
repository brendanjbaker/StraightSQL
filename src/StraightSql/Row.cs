namespace StraightSql
{
	using Entity;
	using System;
	using System.Data.Common;
	using Conversion;

	public class Row
		: IRow
	{
		private readonly DbDataReader reader;
		private readonly ITypeConverter typeConverter;
		private readonly IEntityContext entityConfigurationCollection;

		public Row(DbDataReader reader, ITypeConverter typeConverter, IEntityContext entityConfigurationCollection)
		{
			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			if (typeConverter == null)
				throw new ArgumentNullException(nameof(typeConverter));

			if (entityConfigurationCollection == null)
				throw new ArgumentNullException(nameof(entityConfigurationCollection));

			this.reader = reader;
			this.typeConverter = typeConverter;
			this.entityConfigurationCollection = entityConfigurationCollection;
		}

		public T Read<T>(String columnName)
		{
			if (columnName == null)
				throw new ArgumentNullException(nameof(columnName));

			var value = reader[columnName];

			return typeConverter.Convert<T>(value);
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
