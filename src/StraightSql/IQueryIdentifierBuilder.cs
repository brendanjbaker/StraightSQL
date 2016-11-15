namespace StraightSql
{
	using System;

	public interface IQueryIdentifierBuilder
		: IQueryParameterBuilder
	{
		IQueryParameterBuilder SetIdentifier(UInt32 identifier);
	}
}
