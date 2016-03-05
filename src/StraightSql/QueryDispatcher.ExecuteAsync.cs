namespace StraightSql
{
	using System.Threading.Tasks;

	public partial class QueryDispatcher
	{
		public async Task ExecuteAsync(IQuery query)
		{
			using (var connection = await connectionFactory.CreateAsync())
			{
				using (var command = connection.CreateCommand())
				{
					PrepareCommand(command, query);

					await command.ExecuteNonQueryAsync();
				}
			}
		}
	}
}
