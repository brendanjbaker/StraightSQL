namespace StraightSql
{
	using Npgsql;
	using System;

	public static class QueryParameterBuilderExtensions
	{
		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Guid? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Int32? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, String value)
		{
			return instance.SetParameterInternal(name, value);
		}

		private static IQueryParameterBuilder SetParameterInternal<T>(this IQueryParameterBuilder instance, String name, T value)
		{
			return value == null
				? instance.SetParameter(new NpgsqlParameter(name, DBNull.Value))
				: instance.SetParameter(new NpgsqlParameter(name, value));
		}
	}
}
