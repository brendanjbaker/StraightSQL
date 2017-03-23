namespace StraightSql
{
	using Entity;
	using Npgsql;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class ContextualizedQuery
		: IContextualizedQuery
	{
		private readonly IEntityConfigurationCollection entityConfigurationCollection;
		private readonly IQuery query;
		private readonly IQueryDispatcher queryDispatcher;

		public ContextualizedQuery(IQuery query, IQueryDispatcher queryDispatcher, IEntityConfigurationCollection entityConfigurationCollection)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			if (queryDispatcher == null)
				throw new ArgumentNullException(nameof(queryDispatcher));

			if (entityConfigurationCollection == null)
				throw new ArgumentNullException(nameof(entityConfigurationCollection));

			this.query = query;
			this.queryDispatcher = queryDispatcher;
			this.entityConfigurationCollection = entityConfigurationCollection;
		}

		public UInt32? Identifier => query.Identifier;

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
			where T : new()
		{
			return await queryDispatcher.FirstAsync(query, row => entityConfigurationCollection.Read<T>(row));
		}

		public async Task<T> FirstAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.FirstAsync(query, row => reader(row));
		}

		public async Task<T> FirstOrDefaultAsync<T>()
			where T : new()
		{
			return await queryDispatcher.FirstOrDefaultAsync(query, row => entityConfigurationCollection.Read<T>(row));
		}

		public async Task<T> FirstOrDefaultAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.FirstOrDefaultAsync(query, row => reader(row));
		}

		public async Task<IList<T>> ListAsync<T>()
			where T : new()
		{
			return await queryDispatcher.ListAsync(query, row => entityConfigurationCollection.Read<T>(row));
		}

		public async Task<IList<T>> ListAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.ListAsync(query, row => reader(row));
		}

		public async Task<T> SingleAsync<T>()
			where T : new()
		{
			return await queryDispatcher.SingleAsync(query, row => entityConfigurationCollection.Read<T>(row));
		}

		public async Task<T> SingleAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.SingleAsync(query, row => reader(row));
		}

		public async Task<T> SingleOrDefaultAsync<T>()
			where T : new()
		{
			return await queryDispatcher.SingleOrDefaultAsync(query, row => entityConfigurationCollection.Read<T>(row));
		}

		public async Task<T> SingleOrDefaultAsync<T>(Func<IRow, T> reader)
		{
			return await queryDispatcher.SingleOrDefaultAsync(query, row => reader(row));
		}
	}
}
