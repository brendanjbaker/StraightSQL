namespace StraightSql
{
	using System;

	public interface ITypeConverter
	{
		Boolean TryConvert<T>(Object @object, out T converted);
	}
}
