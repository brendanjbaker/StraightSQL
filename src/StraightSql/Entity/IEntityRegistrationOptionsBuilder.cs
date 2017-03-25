namespace StraightSql.Entity
{
	using System;
	using System.Linq.Expressions;

	public interface IEntityConfigurationOptionsBuilder<TEntity>
	{
		IEntityConfigurationOptionsBuilder<TEntity> AddField<TField>(Expression<Func<TEntity, TField>> expression, String name);
		IEntityConfiguration Build();
	}
}
