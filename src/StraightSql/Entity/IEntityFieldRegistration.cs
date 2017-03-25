namespace StraightSql.Entity
{
	using System;

	public interface IEntityFieldRegistration
	{
		String Name { get; }

		void Apply(Object entity, IRow row);
	}
}
