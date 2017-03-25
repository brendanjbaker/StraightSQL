namespace StraightSql.Entity
{
	using System;

	public class EntityRegistrationNotFoundException
		: Exception
	{
		private readonly Type entityType;

		public EntityRegistrationNotFoundException(Type entityType)
		{
			if (entityType == null)
				throw new ArgumentNullException(nameof(entityType));

			this.entityType = entityType;
		}

		public override String Message
		{
			get { return $"Entity registration for type {entityType.Name} not found."; }
		}
	}
}
