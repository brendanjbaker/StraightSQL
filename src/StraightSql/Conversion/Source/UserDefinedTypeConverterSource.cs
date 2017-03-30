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

		public TypeConverter TryGet<T>(Type type)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			var matchingTypeConverters =
				typeConverters
					.Where(tc => tc.CanConvert(type, typeof(T)))
					.ToArray();

			if (matchingTypeConverters.None())
				return null;

			if (matchingTypeConverters.Multiple())
				throw new MultipleTypeConvertersFoundException(type, typeof(T));

			var typeConverter = matchingTypeConverters.Single();

			return new FunctionalTypeConverter(type, typeof(T), instance =>
			{
				return typeConverter.Convert<T>(instance);
			});
		}
	}
}
