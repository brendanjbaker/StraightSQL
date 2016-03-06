namespace StraightSql
{
	using System;

	public interface IQueryParameterBuilder
	{
		IQueryParameterBuilder SetParameter(String name, Object value);
		IQuery Build();
	}
}
