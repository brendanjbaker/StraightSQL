namespace StraightSql.Conversion.Source
{
	using System;
	using System.ComponentModel;
	using System.Reflection;

	public class AssignableTypeConverterSource
		: ITypeConverterSource
	{
		public TypeConverter TryGet<T>(Type type)
		{
			if (!typeof(T).IsAssignableFrom(type))
				return null;

			return new FunctionalTypeConverter(type, typeof(T), instance =>
			{
				return (T)instance;
			});
		}
	}
}
