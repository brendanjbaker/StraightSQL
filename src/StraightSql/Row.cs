namespace StraightSql
{
	using Conversion;
	using Entity;
	using System;
	using System.Data.Common;

	public class Row
		: IRow
	{
		private readonly DbDataReader reader;
		private readonly ITypeConverter typeConverter;
		private readonly IEntityContext entityContext;

		public Row(DbDataReader reader, ITypeConverter typeConverter, IEntityContext entityContext)
		{
			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			if (typeConverter == null)
				throw new ArgumentNullException(nameof(typeConverter));

			if (entityContext == null)
				throw new ArgumentNullException(nameof(entityContext));

			this.reader = reader;
			this.typeConverter = typeConverter;
			this.entityContext = entityContext;
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

			return entityContext.Read<T>(row);
		}
	}
}
