namespace StraightSql
{
	using System;

	public interface IContextualizedQueryIdentifierBuilder
		: IContextualizedQueryParameterBuilder
    {
		IContextualizedQueryParameterBuilder SetIdentifier(UInt32 identifier);
    }
}
