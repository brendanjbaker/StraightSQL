namespace StraightSql
{
	using System;

	internal static class TypeConverterExtensions
	{
		public static Boolean CanConvert(this System.ComponentModel.TypeConverter typeConverter, Type fromType, Type toType)
		{
			return typeConverter.CanConvertFrom(fromType) && typeConverter.CanConvertTo(toType);
		}

		public static T Convert<T>(this System.ComponentModel.TypeConverter typeConverter, Object instance)
		{
			return (T)typeConverter.ConvertTo(instance, typeof(T));
		}

		public static T Convert<T>(this ITypeConverter typeConverter, Object instance)
		{
			T value;

			if (typeConverter.TryConvert(instance, out value))
				return value;

			throw new InvalidCastException($"Could not convert type {value.GetType().Name} to type {typeof(T).Name}.");
		}
	}
}
