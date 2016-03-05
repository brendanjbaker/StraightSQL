namespace StraightSql
{
	using System;

	public interface IQueryParameterBuilder
	{
		IQueryParameterBuilder SetParameter<T>(String name, T value);
		IQuery Build();
	}
}
