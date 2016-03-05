namespace StraightSql
{
	using System;

	public class QueryParameter
		: IQueryParameter
	{
		private readonly String name;
		private readonly Object value;

		public QueryParameter(String name, Object value)
		{
			this.name = name;
			this.value = value;
		}

		public String Name => name;
		public Object Value => value;
	}
}
