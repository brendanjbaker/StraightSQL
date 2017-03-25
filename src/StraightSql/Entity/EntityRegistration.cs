namespace StraightSql.Entity
{
	using System;
	using System.Collections.Generic;

	public class EntityRegistration
		: IEntityRegistration
	{
		private readonly String name;
		private readonly Type type;
		private readonly IEnumerable<IEntityFieldRegistration> fields;

		public EntityRegistration(String name, Type type, IEnumerable<IEntityFieldRegistration> fields)
		{
			this.name = name;
			this.type = type;
			this.fields = fields;
		}

		public static IEntityRegistration Create<TEntity>(String name, Action<IEntityRegistrationOptionsBuilder<TEntity>> options)
		{
			var fieldsBuilder = new EntityRegistrationOptionsBuilder<TEntity>(name);

			options(fieldsBuilder);

			return fieldsBuilder.Build();
		}

		public IEnumerable<IEntityFieldRegistration> Fields => fields;

		public String Name => name;

		public Type Type => type;
	}
}
