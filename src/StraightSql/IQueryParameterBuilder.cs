namespace StraightSql
{
	using System;

	public interface IQueryParameterBuilder
	{
		IQuery Build();
		IQueryParameterBuilder SetParameter(String name, Object value);
	}
}
