namespace StraightSql.Conversion
{
	using System;

	public class MultipleTypeConvertersFoundException
		: Exception
	{
		private readonly Type fromType;
		private readonly Type toType;

		public MultipleTypeConvertersFoundException(Type fromType, Type toType)
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
			get { return $"Multiple type converters found (from type {fromType.Name} to type {toType.Name})."; }
		}
	}
}
