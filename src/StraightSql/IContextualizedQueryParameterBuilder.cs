namespace StraightSql
{
	using System;

	public interface IContextualizedQueryParameterBuilder
	{
		IContextualizedQuery Build();
		IContextualizedQueryParameterBuilder SetParameter<T>(String name, T value);
	}
}
