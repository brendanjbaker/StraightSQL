namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Diagnostics;
	using System.Threading.Tasks;

	public abstract class TimingQueryExecutor
		: IQueryExecutor
	{
		private readonly IQueryExecutor queryExecutor;

		public TimingQueryExecutor(IQueryExecutor queryExecutor)
		{
			this.queryExecutor = queryExecutor;
		}

		public async Task<T> ExecuteQueryAsync<T>(IQuery query, Func<NpgsqlCommand, Task<T>> functionAsync)
		{
			var stopwatch = new Stopwatch();

			stopwatch.Start();

			var result = await queryExecutor.ExecuteQueryAsync(query, functionAsync);

			stopwatch.Stop();

			await OnQueryCompletionAsync(query, stopwatch.Elapsed);

			return result;
		}

		protected abstract Task OnQueryCompletionAsync(IQuery query, TimeSpan duration);
	}
}
