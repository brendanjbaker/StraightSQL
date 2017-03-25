namespace StraightSql.Entity
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;

	public class EntityFieldRegistration
		: IEntityFieldRegistration
	{
		private readonly Action<Object, IRow> applyAction;
		private readonly String name;

		public EntityFieldRegistration(String name, Action<Object, IRow> applyAction)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));

			if (applyAction == null)
				throw new ArgumentNullException(nameof(applyAction));

			this.applyAction = applyAction;
			this.name = name;
		}

		public String Name => name;

		public static EntityFieldRegistration Create<TEntity, TField>(Expression<Func<TEntity, TField>> expression, String name)
		{
			if (expression == null)
				throw new ArgumentNullException(nameof(expression));

			if (name == null)
				throw new ArgumentNullException(nameof(name));

			var memberExpression = (MemberExpression)expression.Body;
			var property = (PropertyInfo)memberExpression.Member;

			return new EntityFieldRegistration(name, (entity, row) =>
			{
				property.SetValue(entity, row.Read<TField>(name));
			});
		}

		public void Apply(Object entity, IRow row)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			if (row == null)
				throw new ArgumentNullException(nameof(row));

			applyAction(entity, row);
		}
	}
}
