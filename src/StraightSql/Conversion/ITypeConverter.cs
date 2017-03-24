namespace StraightSql.Conversion
{
	using System;

	public interface ITypeConverter
	{
		T Convert<T>(Object @instance);
	}
}
