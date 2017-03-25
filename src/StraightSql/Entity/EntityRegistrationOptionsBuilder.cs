namespace StraightSql.Entity
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public class EntityRegistrationOptionsBuilder<TEntity>
		: IEntityRegistrationOptionsBuilder<TEntity>
	{
		private readonly String name;
		private readonly ICollection<IEntityFieldRegistration> fieldRegistrations;

		public EntityRegistrationOptionsBuilder(String name)
			: this(name, new List<IEntityFieldRegistration>()) { }

		public EntityRegistrationOptionsBuilder(String name, ICollection<IEntityFieldRegistration> fieldRegistrations)
		{
			this.name = name;
			this.fieldRegistrations = fieldRegistrations;
		}

		public IEntityRegistrationOptionsBuilder<TEntity> AddField<TField>(Expression<Func<TEntity, TField>> expression, String name)
		{
			fieldRegistrations.Add(EntityFieldRegistration.Create(expression, name));

			return this;
		}

		public IEntityRegistration Build()
		{
			return new EntityRegistration(name, typeof(TEntity), fieldRegistrations);
		}
	}
}
