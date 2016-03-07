namespace StraightSql
{
	using Npgsql;
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

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, Boolean? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, Byte? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, Byte[] value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, DateTime? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, Guid? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, Int16? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, Int32? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, Int64? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, String value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, UInt16? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, UInt32? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IContextualizedQueryParameterBuilder SetParameter(this IContextualizedQueryParameterBuilder instance, String name, UInt64? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static async Task<T> SingleAsync<T>(this IContextualizedQueryParameterBuilder builder)
		{
			return await builder.Build().SingleAsync<T>();
		}

		public static async Task<T> SingleOrDefaultAsync<T>(this IContextualizedQueryParameterBuilder builder)
		{
			return await builder.Build().SingleOrDefaultAsync<T>();
		}

		private static IContextualizedQueryParameterBuilder SetParameterInternal<T>(this IContextualizedQueryParameterBuilder instance, String name, T value)
		{
			return value == null
				? instance.SetParameter(new NpgsqlParameter(name, DBNull.Value))
				: instance.SetParameter(new NpgsqlParameter(name, value));
		}
	}
}
