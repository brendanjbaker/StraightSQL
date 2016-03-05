namespace StraightSql
{
	using System;

	public class ReaderNotFoundException
		: Exception
	{
		private readonly Type readerType;

		public ReaderNotFoundException(Type readerType)
		{
			this.readerType = readerType;
		}

		public override String Message
		{
			get { return $"Reader for type {readerType.Name} not found."; }
		}
	}
}
