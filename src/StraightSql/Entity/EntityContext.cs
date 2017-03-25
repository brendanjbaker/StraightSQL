namespace StraightSql.Entity
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class EntityContext
		: IEntityContext
	{
		private readonly IEnumerable<IEntityRegistration> entityRegistrations;

		public EntityContext(IEnumerable<IEntityRegistration> entityRegistrations)
		{
			this.entityRegistrations = entityRegistrations;
		}

		public IEntityRegistration Get<TEntity>()
		{
			var entityRegistration = entityRegistrations.SingleOrDefault(es => es.Type == typeof(TEntity));

			if (entityRegistration == null)
				throw new EntityRegistrationNotFoundException(typeof(TEntity));

			return entityRegistration;
		}

		public TEntity Read<TEntity>(IRow row)
			where TEntity : new()
		{
			if (row == null)
				throw new ArgumentNullException(nameof(row));

			var entityRegistration = Get<TEntity>();
			var entity = new TEntity();

			foreach (var field in entityRegistration.Fields)
			{
				field.Apply(entity, row);
			}

			return entity;
		}
	}
}
