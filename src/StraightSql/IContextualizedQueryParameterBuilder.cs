namespace StraightSql
{
	using System;

	public interface IContextualizedQueryParameterBuilder
	{
		IContextualizedQuery Build();
		IContextualizedQueryParameterBuilder SetParameter(String name, Object value);
	}
}
