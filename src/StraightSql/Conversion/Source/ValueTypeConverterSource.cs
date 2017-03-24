namespace StraightSql.Conversion.Source
{
	using System;
	using System.ComponentModel;

	public class ValueTypeConverterSource
		: ITypeConverterSource
	{
		public TypeConverter TryGet<T>(Object instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			if (instance.GetType() != typeof(T))
				return null;

			return new FunctionalTypeConverter(instance.GetType(), typeof(T), (localInstance) =>
			{
				return (T)localInstance;
			});
		}
	}
}
