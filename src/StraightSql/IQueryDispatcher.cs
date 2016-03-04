namespace StraightSql
{
	using System.Threading.Tasks;

	public interface IQueryDispatcher
	{
		Task ExecuteAsync(IQuery query);
	}
}
