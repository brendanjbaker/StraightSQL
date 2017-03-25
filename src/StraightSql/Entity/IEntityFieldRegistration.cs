namespace StraightSql.Entity
{
	using System;

	public interface IEntityFieldConfiguration
	{
		String Name { get; }

		void Apply(Object entity, IRow row);
	}
}
