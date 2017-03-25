namespace StraightSql.Entity
{
	using System;
	using System.Collections.Generic;

	public interface IEntityRegistration
	{
		IEnumerable<IEntityFieldRegistration> Fields { get; }
		String Name { get; }
		Type Type { get; }
	}
}
