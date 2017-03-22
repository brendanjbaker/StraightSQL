namespace StraightSql.Entity
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public class EntityConfigurationOptionsBuilder<TEntity>
		: IEntityConfigurationOptionsBuilder<TEntity>
	{
		private readonly String name;
		private readonly ICollection<IEntityFieldConfiguration> fieldConfigurations;

		public EntityConfigurationOptionsBuilder(String name)
			: this(name, new List<IEntityFieldConfiguration>()) { }

		public EntityConfigurationOptionsBuilder(String name, ICollection<IEntityFieldConfiguration> fieldConfigurations)
		{
			this.name = name;
			this.fieldConfigurations = fieldConfigurations;
		}

		public IEntityConfigurationOptionsBuilder<TEntity> AddField<TField>(Expression<Func<TEntity, TField>> expression, String name)
		{
			fieldConfigurations.Add(EntityFieldConfiguration.Create(expression, name));

			return this;
		}

		public IEntityConfiguration Build()
		{
			return new EntityConfiguration(name, typeof(TEntity), fieldConfigurations);
		}
	}
}
