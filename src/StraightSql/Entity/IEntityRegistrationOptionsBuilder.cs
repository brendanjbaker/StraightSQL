namespace StraightSql.Entity
{
	using System;
	using System.Linq.Expressions;

	public interface IEntityRegistrationOptionsBuilder<TEntity>
	{
		IEntityRegistrationOptionsBuilder<TEntity> AddField<TField>(Expression<Func<TEntity, TField>> expression, String name);
		IEntityRegistration Build();
	}
}
