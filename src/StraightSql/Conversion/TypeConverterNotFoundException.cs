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
			get { return $"Could not convert type from {fromType.Name} to {toType.Name}."; }
		}
	}
}
