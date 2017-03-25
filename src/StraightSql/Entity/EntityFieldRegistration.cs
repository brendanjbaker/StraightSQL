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
			this.applyAction = applyAction;
			this.name = name;
		}

		public String Name => name;

		public static EntityFieldRegistration Create<TEntity, TField>(Expression<Func<TEntity, TField>> expression, String name)
		{
			var memberExpression = (MemberExpression)expression.Body;
			var property = (PropertyInfo)memberExpression.Member;

			return new EntityFieldRegistration(name, (entity, row) =>
			{
				property.SetValue(entity, row.Read<TField>(name));
			});
		}

		public void Apply(Object entity, IRow row)
		{
			applyAction(entity, row);
		}
	}
}
