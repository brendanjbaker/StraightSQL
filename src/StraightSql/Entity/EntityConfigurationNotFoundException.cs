namespace StraightSql.Entity
{
	using System;

	public class EntityConfigurationNotFoundException
		: Exception
	{
		private readonly Type entityType;

		public EntityConfigurationNotFoundException(Type entityType)
		{
			if (entityType == null)
				throw new ArgumentNullException(nameof(entityType));

			this.entityType = entityType;
		}

		public override String Message
		{
			get { return $"Entity configuration for type {entityType.Name} not found."; }
		}
	}
}
