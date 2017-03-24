namespace StraightSql
{
	using System;
	using System.ComponentModel;

	internal static class TypeConverterExtensions
	{
		public static Boolean CanConvert(this TypeConverter typeConverter, Type fromType, Type toType)
		{
			return typeConverter.CanConvertFrom(fromType) && typeConverter.CanConvertTo(toType);
		}

		public static T Convert<T>(this TypeConverter typeConverter, Object instance)
		{
			return (T)typeConverter.ConvertTo(instance, typeof(T));
		}
	}
}
