namespace StraightSql.Entity
{
	public interface IEntityContext
	{
		IEntityConfiguration Get<TEntity>();

		TEntity Read<TEntity>(IRow row)
			where TEntity : new();
	}
}
