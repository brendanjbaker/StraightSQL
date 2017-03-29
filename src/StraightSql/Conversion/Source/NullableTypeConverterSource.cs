namespace StraightSql.Conversion.Source
{
	using System;
	using System.ComponentModel;

	public class NullableTypeConverterSource
		: ITypeConverterSource
	{
		public TypeConverter TryGet<T>(Type type)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			var nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(T));

			if (nullableUnderlyingType == null)
				return null;

			if (type != nullableUnderlyingType)
				return null;

			return new FunctionalTypeConverter(type, typeof(T), localInstance =>
			{
				return (T)localInstance;
			});
		}
	}
}
