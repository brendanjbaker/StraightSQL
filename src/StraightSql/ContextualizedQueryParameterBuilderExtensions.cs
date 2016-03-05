namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public static class ContextualizedQueryParameterBuilderExtensions
	{
		public static async Task<Boolean> AnyAsync(this IContextualizedQueryParameterBuilder builder)
		{
			return await builder.Build().AnyAsync();
		}

		public static async Task<Int64> CountAsync(this IContextualizedQueryParameterBuilder builder)
		{
			return await builder.Build().CountAsync();
		}

		public static async Task ExecuteAsync(this IContextualizedQueryParameterBuilder builder)
		{
			await builder.Build().ExecuteAsync();
		}

		public static async Task<T> FirstAsync<T>(this IContextualizedQueryParameterBuilder builder)
		{
			return await builder.Build().FirstAsync<T>();
		}

		public static async Task<T> FirstOrDefaultAsync<T>(this IContextualizedQueryParameterBuilder builder)
		{
			return await builder.Build().FirstOrDefaultAsync<T>();
		}

		public static async Task<IList<T>> ListAsync<T>(this IContextualizedQueryParameterBuilder builder)
		{
			return await builder.Build().ListAsync<T>();
		}

		public static async Task<T> SingleAsync<T>(this IContextualizedQueryParameterBuilder builder)
		{
			return await builder.Build().SingleAsync<T>();
		}

		public static async Task<T> SingleOrDefaultAsync<T>(this IContextualizedQueryParameterBuilder builder)
		{
			return await builder.Build().SingleOrDefaultAsync<T>();
		}
	}
}
