namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Threading.Tasks;

	public class ContextualizedQuery
		: IContextualizedQuery
	{
		private readonly IQuery query;
		private readonly IQueryDispatcher queryDispatcher;

		public ContextualizedQuery(IQuery query, IQueryDispatcher queryDispatcher)
		{
			this.query = query;
			this.queryDispatcher = queryDispatcher;
		}

		public IEnumerable<NpgsqlParameter> Parameters
		{
			get { return query.Parameters; }
		}

		public String Text
		{
			get { return query.Text; }
		}

		public async Task<Boolean> AnyAsync()
		{
			return await queryDispatcher.AnyAsync(query);
		}

		public async Task<Int64> CountAsync()
		{
			return await queryDispatcher.CountAsync(query);
		}

		public async Task ExecuteAsync()
		{
			await queryDispatcher.ExecuteAsync(query);
		}

		public async Task<T> FirstAsync<T>(Func<DbDataReader, T> reader)
		{
			return await queryDispatcher.FirstAsync(query, reader);
		}

		public async Task<T> FirstOrDefaultAsync<T>(Func<DbDataReader, T> reader)
		{
			return await queryDispatcher.FirstOrDefaultAsync(query, reader);
		}

		public async Task<IList<T>> ListAsync<T>(Func<DbDataReader, T> reader)
		{
			return await queryDispatcher.ListAsync(query, reader);
		}

		public async Task<T> SingleAsync<T>(Func<DbDataReader, T> reader)
		{
			return await queryDispatcher.SingleAsync(query, reader);
		}

		public async Task<T> SingleOrDefaultAsync<T>(Func<DbDataReader, T> reader)
		{
			return await queryDispatcher.SingleOrDefaultAsync(query, reader);
		}
	}
}
