﻿namespace StraightSql
{
	using Entity;
	using System;
	using System.Data.Common;

	public class Row
		: IRow
	{
		private readonly DbDataReader reader;
		private readonly ITypeConverter typeConverter;
		private readonly IEntityConfigurationCollection entityConfigurationCollection;

		public Row(DbDataReader reader, ITypeConverter typeConverter, IEntityConfigurationCollection entityConfigurationCollection)
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

			if (value == DBNull.Value)
				return default(T);

			if (value.GetType() == typeof(T))
				return (T)value;

			var nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(T));

			if (nullableUnderlyingType != null)
			{
				if (value.GetType() == nullableUnderlyingType)
					return (T)value;
			}

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
