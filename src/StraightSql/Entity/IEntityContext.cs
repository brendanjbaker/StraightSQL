namespace StraightSql.Entity
{
	public interface IEntityConfigurationCollection
	{
		IEntityConfiguration Get<TEntity>();

		TEntity Read<TEntity>(IRow row)
			where TEntity : new();
	}
}
