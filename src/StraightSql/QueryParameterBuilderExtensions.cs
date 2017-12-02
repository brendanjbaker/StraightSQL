namespace StraightSql
{
	using Npgsql;
	using System;

	public static class QueryParameterBuilderExtensions
	{
		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Boolean? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Byte? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Byte[] value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, DateTime? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Decimal? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Guid? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Int16? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Int32? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, Int64? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, String value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, UInt16? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, UInt32? value)
		{
			return instance.SetParameterInternal(name, value);
		}

		public static IQueryParameterBuilder SetParameter(this IQueryParameterBuilder instance, String name, UInt64? value)
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
