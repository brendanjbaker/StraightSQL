namespace StraightSql.Composition.SimpleInjector
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	public class StraightSqlOptions
	{
		public StraightSqlOptions()
		{
			TypeConverters = new List<TypeConverter>();
		}

		public Func<IServiceProvider, String> ConnectionStringFactory { get; set; }
		public ICollection<TypeConverter> TypeConverters { get; set; }
	}
}
