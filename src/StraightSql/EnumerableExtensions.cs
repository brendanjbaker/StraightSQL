﻿namespace StraightSql
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class EnumerableExtensions
	{
		public static Boolean Multiple<T>(this IEnumerable<T> source)
		{
			return source.Skip(1).Any();
		}

		public static Boolean None<T>(this IEnumerable<T> source)
		{
			return !source.Any();
		}
	}
}
