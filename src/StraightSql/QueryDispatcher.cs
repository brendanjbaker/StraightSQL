namespace StraightSql
{
	using Entity;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Threading.Tasks;
	using Conversion;

	public class QueryDispatcher
		: IQueryDispatcher
	{
		private readonly IQueryExecutor queryExecutor;
		private readonly ITypeConverter typeConverter;
		private readonly IEntityContext entityContext;

		public QueryDispatcher(IQueryExecutor queryExecutor, ITypeConverter typeConverter, IEntityContext entityContext)
		{
			if (queryExecutor == null)
				throw new ArgumentNullException(nameof(queryExecutor));

			if (typeConverter == null)
				throw new ArgumentNullException(nameof(typeConverter));

			if (entityContext == null)
				throw new ArgumentNullException(nameof(entityContext));

			this.queryExecutor = queryExecutor;
			this.typeConverter = typeConverter;
			this.entityContext = entityContext;
		}

		public async Task<Boolean> AnyAsync(IQuery query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			var first = await FirstOrDefaultAsync(query, reader =>
			{
				return new Object();
			});

			return first != null;
		}

		public async Task<Int64> CountAsync(IQuery query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return await ExecuteScalarAsync<Int64>(query);
		}

		public async Task ExecuteAsync(IQuery query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			await queryExecutor.ExecuteQueryAsync(query, async command =>
			{
				await command.ExecuteNonQueryAsync();

				return 0;
			});
		}

		public async Task<T> ExecuteScalarAsync<T>(IQuery query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return await queryExecutor.ExecuteQueryAsync(query, async command =>
			{
				var scalarObject = await command.ExecuteScalarAsync();
				var scalar = (T)scalarObject;

				return scalar;
			});
		}

		public async Task<T> FirstAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			var result = await FirstOrDefaultAsync(query, reader);

			if (result == null)
				throw new InvalidOperationException("The sequence contained no elements.");

			return result;
		}

		public async Task<T> FirstOrDefaultAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			return await queryExecutor.ExecuteQueryAsync(query, async command =>
			{
				var dataReader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

				if (!await dataReader.ReadAsync())
					return default(T);

				return reader(new Row(dataReader, typeConverter, entityContext));
			});
		}

		public async Task<IList<T>> ListAsync<T>(IQuery query, Func<IRow, T> readerFunction)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			if (readerFunction == null)
				throw new ArgumentNullException(nameof(readerFunction));

			return await queryExecutor.ExecuteQueryAsync(query, async command =>
			{
				var list = new List<T>();
				var dataReader = await command.ExecuteReaderAsync();

				while (await dataReader.ReadAsync() != false)
				{
					list.Add(readerFunction(new Row(dataReader, typeConverter, entityContext)));
				}

				return list;
			});
		}

		public async Task<T> SingleAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			var result = await SingleOrDefaultAsync(query, reader);

			if (result == null)
				throw new InvalidOperationException("The sequence did not contain exactly one element.");

			return result;
		}

		public async Task<T> SingleOrDefaultAsync<T>(IQuery query, Func<IRow, T> reader)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			if (reader == null)
				throw new ArgumentNullException(nameof(reader));

			return await queryExecutor.ExecuteQueryAsync(query, async command =>
			{
				var dataReader = await command.ExecuteReaderAsync();

				if (!await dataReader.ReadAsync())
					return default(T);

				var first = reader(new Row(dataReader, typeConverter, entityContext));

				if (await dataReader.ReadAsync())
					return default(T);

				return first;
			});
		}
	}
}
