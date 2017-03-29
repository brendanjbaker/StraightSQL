namespace StraightSql.Conversion.Source
{
	using System;
	using System.ComponentModel;

	public class ValueTypeConverterSource
		: ITypeConverterSource
	{
		public TypeConverter TryGet<T>(Type type)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			if (type != typeof(T))
				return null;

			return new FunctionalTypeConverter(type, typeof(T), (localInstance) =>
			{
				return (T)localInstance;
			});
		}
	}
}
