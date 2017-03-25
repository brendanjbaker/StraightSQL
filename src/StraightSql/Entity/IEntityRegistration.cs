namespace StraightSql.Entity
{
	using System;
	using System.Collections.Generic;

	public interface IEntityConfiguration
	{
		IEnumerable<IEntityFieldConfiguration> Fields { get; }
		String Name { get; }
		Type Type { get; }
	}
}
