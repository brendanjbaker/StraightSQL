namespace StraightSql
{
	using Npgsql;
	using System;

	public class UniqueIndexViolationException
		: Exception
	{
		public UniqueIndexViolationException(PostgresException innerException)
			: base("A unique index constraint was violated.", innerException)
		{ }
	}
}
