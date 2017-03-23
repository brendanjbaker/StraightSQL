namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class TypeConverter
		: ITypeConverter
	{
		private readonly IEnumerable<System.ComponentModel.TypeConverter> typeConverters;

		public TypeConverter(IEnumerable<System.ComponentModel.TypeConverter> typeConverters)
		{
			this.typeConverters = typeConverters;
		}

		public Boolean TryConvert<T>(Object instance, out T converted)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			converted = default(T);

			var typeConverter = typeConverters.SingleOrDefault(tc => tc.CanConvert(instance.GetType(), typeof(T)));

			if (typeConverter == null)
				return false;

			converted = typeConverter.Convert<T>(instance);

			return true;
		}
	}
}
