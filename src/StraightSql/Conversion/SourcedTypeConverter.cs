namespace StraightSql.Conversion
{
	using System;

	public class SourcedTypeConverter
		: ITypeConverter
	{
		private readonly ITypeConverterSource typeConverterSource;

		public SourcedTypeConverter(ITypeConverterSource typeConverterSource)
		{
			if (typeConverterSource == null)
				throw new ArgumentNullException(nameof(typeConverterSource));

			this.typeConverterSource = typeConverterSource;
		}

		public T Convert<T>(Object instance)
		{
			if (instance == null)
				return default(T);

			if (instance == DBNull.Value)
				return default(T);

			var typeConverter = typeConverterSource.TryGet<T>(instance);

			if (typeConverter == null)
				throw new TypeConverterNotFoundException(instance.GetType(), typeof(T));

			return typeConverter.Convert<T>(instance);
		}
	}
}
