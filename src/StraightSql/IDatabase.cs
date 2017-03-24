namespace StraightSql
{
	using System;

	public interface IDatabase
	{
		IContextualizedQueryIdentifierBuilder CreateQuery(String query);
		String GetColumnNames<TEntity>(String tablePrefix = null);
	}
}
