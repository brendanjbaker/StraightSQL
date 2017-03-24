namespace StraightSql.Conversion.Source
{
	using System;
	using System.ComponentModel;

	public class NullableTypeConverterSource
		: ITypeConverterSource
	{
		public TypeConverter TryGet<T>(Object instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			var nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(T));

			if (nullableUnderlyingType == null)
				return null;

			if (instance.GetType() != nullableUnderlyingType)
				return null;

			return new FunctionalTypeConverter(instance.GetType(), typeof(T), (localInstance) =>
			{
				return (T)localInstance;
			});
		}
	}
}
