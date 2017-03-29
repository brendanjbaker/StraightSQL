namespace StraightSql.Conversion.Source
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	public class CachingTypeConverterSource
		: ITypeConverterSource
	{
		private readonly IDictionary<Type, TypeConverter> typeConverterCache;
		private readonly ITypeConverterSource typeConverterSource;

		public CachingTypeConverterSource(ITypeConverterSource typeConverterSource)
			: this(new Dictionary<Type, TypeConverter>(), typeConverterSource) { }

		public CachingTypeConverterSource(
			IDictionary<Type, TypeConverter> typeConverterCache,
			ITypeConverterSource typeConverterSource)
		{
			if (typeConverterCache == null)
				throw new ArgumentNullException(nameof(typeConverterCache));

			if (typeConverterSource == null)
				throw new ArgumentNullException(nameof(typeConverterSource));

			this.typeConverterCache = typeConverterCache;
			this.typeConverterSource = typeConverterSource;
		}

		public TypeConverter TryGet<T>(Type type)
		{
			TypeConverter typeConverter;

			if (type == null)
				throw new ArgumentNullException(nameof(type));

			if (typeConverterCache.TryGetValue(typeof(T), out typeConverter))
				return typeConverter;

			typeConverter = typeConverterSource.TryGet<T>(type);

			if (typeConverter != null)
				lock (typeConverterCache)
					if (!typeConverterCache.ContainsKey(typeof(T)))
						typeConverterCache.Add(typeof(T), typeConverter);

			return typeConverter;
		}
	}
}
