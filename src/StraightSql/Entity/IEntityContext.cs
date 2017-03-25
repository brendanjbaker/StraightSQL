namespace StraightSql.Entity
{
	public interface IEntityContext
	{
		IEntityRegistration Get<TEntity>();

		TEntity Read<TEntity>(IRow row)
			where TEntity : new();
	}
}
