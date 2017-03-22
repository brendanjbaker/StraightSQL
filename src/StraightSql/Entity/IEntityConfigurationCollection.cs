namespace StraightSql.Entity
{
	public interface IEntityConfigurationCollection
	{
		TEntity Read<TEntity>(IRow row)
			where TEntity : new();
	}
}
