namespace StraightSql.Entity
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class EntityConfigurationCollection
		: IEntityConfigurationCollection
	{
		private readonly IEnumerable<IEntityConfiguration> entityConfigurations;

		public EntityConfigurationCollection(IEnumerable<IEntityConfiguration> entityConfigurations)
		{
			this.entityConfigurations = entityConfigurations;
		}

		public TEntity Read<TEntity>(IRow row)
			where TEntity : new()
		{
			if (row == null)
				throw new ArgumentNullException(nameof(row));

			var entityConfiguration = entityConfigurations.SingleOrDefault(es => es.Type == typeof(TEntity));

			if (entityConfiguration == null)
				throw new EntityConfigurationNotFoundException(typeof(TEntity));

			var entity = new TEntity();

			foreach (var field in entityConfiguration.Fields)
			{
				field.Apply(entity, row);
			}

			return entity;
		}
	}
}
