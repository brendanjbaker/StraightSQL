namespace StraightSql.Conversion.Source
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public class UserDefinedTypeConverterSource
		: ITypeConverterSource
	{
		private readonly IEnumerable<TypeConverter> typeConverters;

		public UserDefinedTypeConverterSource(IEnumerable<TypeConverter> typeConverters)
		{
			if (typeConverters == null)
				throw new ArgumentNullException(nameof(typeConverters));

			this.typeConverters = typeConverters;
		}

		public TypeConverter TryGet<T>(Object instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			var typeConverter = typeConverters.SingleOrDefault(tc => tc.CanConvert(instance.GetType(), typeof(T)));

			if (typeConverter == null)
				return null;

			return new FunctionalTypeConverter(instance.GetType(), typeof(T), (localInstance) =>
			{
				return typeConverter.Convert<T>(localInstance);
			});
		}
	}
}
