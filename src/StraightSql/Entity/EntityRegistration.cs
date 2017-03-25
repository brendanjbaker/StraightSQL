namespace StraightSql.Entity
{
	using System;
	using System.Collections.Generic;

	public class EntityConfiguration
		: IEntityConfiguration
	{
		private readonly String name;
		private readonly Type type;
		private readonly IEnumerable<IEntityFieldConfiguration> fields;

		public EntityConfiguration(String name, Type type, IEnumerable<IEntityFieldConfiguration> fields)
		{
			this.name = name;
			this.type = type;
			this.fields = fields;
		}

		public static IEntityConfiguration Create<TEntity>(String name, Action<IEntityConfigurationOptionsBuilder<TEntity>> options)
		{
			var fieldsBuilder = new EntityConfigurationOptionsBuilder<TEntity>(name);

			options(fieldsBuilder);

			return fieldsBuilder.Build();
		}

		public IEnumerable<IEntityFieldConfiguration> Fields => fields;

		public String Name => name;

		public Type Type => type;
	}
}
