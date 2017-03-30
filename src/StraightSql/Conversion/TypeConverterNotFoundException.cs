namespace StraightSql.Conversion
{
	using System;

	public class TypeConverterNotFoundException
		: Exception
	{
		private readonly Type fromType;
		private readonly Type toType;

		public TypeConverterNotFoundException(Type fromType, Type toType)
		{
			if (fromType == null)
				throw new ArgumentNullException(nameof(fromType));

			if (toType == null)
				throw new ArgumentNullException(nameof(toType));

			this.fromType = fromType;
			this.toType = toType;
		}

		public override String Message
		{
			get { return $"No type converter found (from type {fromType.Name} to type {toType.Name})."; }
		}
	}
}
