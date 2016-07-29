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

			var queryText = query.Text;

			foreach (var literal in query.Literals)
			{
				var moniker = $":{literal.Key}";

				if (!queryText.Contains(moniker))
					throw new LiteralNotFoundException(literal.Key);

				queryText = queryText.Replace(moniker, literal.Value);
			}

			npgsqlCommand.CommandText = queryText;

			foreach (var queryParameter in query.Parameters)
			{
				npgsqlCommand.Parameters.Add(queryParameter);
			}
		}
	}
}
