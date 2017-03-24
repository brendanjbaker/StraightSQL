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

		public TypeConverter TryGet<T>(Object instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			foreach (var typeConverterSource in typeConverterSources)
			{
				var typeConverter = typeConverterSource.TryGet<T>(instance);

				if (typeConverter != null)
					return typeConverter;
			}

			return null;
		}
	}
}
