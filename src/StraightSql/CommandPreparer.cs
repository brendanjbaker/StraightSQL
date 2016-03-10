namespace StraightSql
{
	using Npgsql;
	using System;

	public class CommandPreparer
		: ICommandPreparer
	{
		public void Prepare(NpgsqlCommand npgsqlCommand, IQuery query)
		{
			if (npgsqlCommand == null)
				throw new ArgumentNullException(nameof(npgsqlCommand));

			if (query == null)
				throw new ArgumentNullException(nameof(query));

			npgsqlCommand.CommandText = query.Text;

			foreach (var queryParameter in query.Parameters)
			{
				npgsqlCommand.Parameters.Add(queryParameter);
			}
		}
	}
}
