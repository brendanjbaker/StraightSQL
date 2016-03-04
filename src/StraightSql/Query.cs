namespace StraightSql
{
	using System;

	public class Query
		: IQuery
	{
		private readonly String text;

		public Query(String text)
		{
			this.text = text;
		}

		public String Text
		{
			get { return text; }
		}
	}
}
