namespace StraightSql
{
	using System;

	public interface IQueryBuilder
	{
		IQueryIdentifierBuilder SetQuery(String query);
	}
}
