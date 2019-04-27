namespace StraightSql.Core
{
	public interface IFeatureCollection
	{
		TFeature TryGetFeature<TFeature>();
	}
}
