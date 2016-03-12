namespace StraightSql
{
	using System;

	public class ReaderTypeMismatchException
		: Exception
	{
		private readonly Type actualType;
		private readonly Type expectedType;

		public ReaderTypeMismatchException(Type expectedType, Type actualType)
		{
			if (expectedType == null)
				throw new ArgumentNullException(nameof(expectedType));

			if (actualType == null)
				throw new ArgumentNullException(nameof(actualType));

			this.actualType = actualType;
			this.expectedType = expectedType;
		}

		public override String Message
		{
			get
			{
				return $"Reader result of type {actualType.Name} did not match expected type {expectedType.Name}.";
			}
		}
	}
}
