namespace StraightSql
{
	using System;

	public class LiteralNotFoundException
		: Exception
	{
		private readonly String name;

		public LiteralNotFoundException(String name)
		{
			this.name = name;
		}

		public override String Message
		{
			get
			{
				return $"The literal \"{name}\" was not found in the query string.";
			}
		}
	}
}
