namespace StraightSql.Conversion.Source
{
	using System;
	using System.ComponentModel;

	public class CascadingTypeConverterSource
		: ITypeConverterSource
	{
		private readonly ITypeConverterSource[] typeConverterSources;

		public CascadingTypeConverterSource(params ITypeConverterSource[] typeConverterSources)
		{
			if (typeConverterSources == null)
				throw new ArgumentNullException(nameof(typeConverterSources));

			this.typeConverterSources = typeConverterSources;
		}

		public TypeConverter TryGet<T>(Type type)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			foreach (var typeConverterSource in typeConverterSources)
			{
				var typeConverter = typeConverterSource.TryGet<T>(type);

				if (typeConverter != null)
					return typeConverter;
			}

			return null;
		}
	}
}
