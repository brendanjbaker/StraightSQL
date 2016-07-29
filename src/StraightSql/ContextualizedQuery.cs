namespace StraightSql
{
	using Npgsql;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class ContextualizedQuery
		: IContextualizedQuery
	{
		private readonly IQuery query;
		private readonly IQueryDispatcher queryDispatcher;
		private readonly IReaderCollection readerCollection;

		public ContextualizedQuery(IQuery query, IQueryDispatcher queryDispatcher, IReaderCollection readerCollection)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (readerCollection == null)
				throw new ArgumentNullException(nameof(readerCollection));

			this.query = query;
			this.queryDispatcher = queryDispatcher;
			this.readerCollection = readerCollection;
		}

		public IDictionary<String, String> Literals => query.Literals;

		public IEnumerable<NpgsqlParameter> Parameters => query.Parameters;

		public String Text => query.Text;

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

		public async Task<T> ExecuteScalarAsync<T>()
		{
			return await queryDispatcher.ExecuteScalarAsync<T>(query);
		}

		public async Task<T> FirstAsync<T>()
		{
			return await queryDispatcher.FirstAsync(query, reader => readerCollection.Read<T>(reader));
		}

		public async Task<T> FirstAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.FirstAsync(query, row => reader(row));
		}

		public async Task<T> FirstOrDefaultAsync<T>()
		{
			return await queryDispatcher.FirstOrDefaultAsync(query, reader => readerCollection.Read<T>(reader));
		}

		public async Task<T> FirstOrDefaultAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.FirstOrDefaultAsync(query, row => reader(row));
		}

		public async Task<IList<T>> ListAsync<T>()
		{
			return await queryDispatcher.ListAsync(query, reader => readerCollection.Read<T>(reader));
		}

		public async Task<IList<T>> ListAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.ListAsync(query, row => reader(row));
		}

		public async Task<T> SingleAsync<T>()
		{
			return await queryDispatcher.SingleAsync(query, reader => readerCollection.Read<T>(reader));
		}

		public async Task<T> SingleAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.SingleAsync(query, row => reader(row));
		}

		public async Task<T> SingleOrDefaultAsync<T>()
		{
			return await queryDispatcher.SingleOrDefaultAsync(query, reader => readerCollection.Read<T>(reader));
		}

		public async Task<T> SingleOrDefaultAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.SingleOrDefaultAsync(query, row => reader(row));
		}
	}
}
