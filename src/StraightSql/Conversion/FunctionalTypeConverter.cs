namespace StraightSql.Conversion
{
	using System;
	using System.ComponentModel;
	using System.Globalization;

	public class FunctionalTypeConverter
		: TypeConverter
	{
		private readonly Type fromType;
		private readonly Type toType;
		private readonly Func<Object, Object> convert;

		public FunctionalTypeConverter(Type fromType, Type toType, Func<Object, Object> convert)
		{
			if (fromType == null)
				throw new ArgumentNullException(nameof(fromType));

			if (toType == null)
				throw new ArgumentNullException(nameof(toType));

			if (convert == null)
				throw new ArgumentNullException(nameof(convert));

			this.fromType = fromType;
			this.toType = toType;
			this.convert = convert;
		}

		public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
		{
			return convert(value);
		}
	}
}
