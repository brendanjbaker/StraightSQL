namespace StraightSql.Conversion
{
	using System;
	using System.ComponentModel;

	public interface ITypeConverterSource
	{
		TypeConverter TryGet<T>(Type type);
	}
}
