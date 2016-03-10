## StraightSQL

Go from this:

```
public async Task<EmailAddressEntity> TryReadAsync(Guid id)
{
	using (var connection = new NpgsqlConnection(connectionString))
	{
		await connectionString.OpenAsync();

		using (var command = connection.CreateCommand())
		{
			command.CommandText = @"
				SELECT *
				FROM ""email_address"" AS ea
				WHERE ea.id = :id";

			command.Parameters.AddWithValue("id", (Guid)id);

			var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleResult);

			if (!await reader.ReadAsync())
				return null;

			return new EmailAddressEntity()
			{
				CreateRequestId = (Guid)reader["create_request_id"],
				CreateTime = (DateTime)reader["create_time"],
				EmailAddressVerificationId = (Guid)reader["email_address_verification_id"],
				Id = (Guid)reader["id"],
				LastUpdateRequestId = (Guid)reader["last_update_request_id"],
				LastUpdateTime = (DateTime)reader["last_update_time"],
				UserId = (Guid)reader["user_id"],
				Value = (String)reader["value"]
			};
		}
	}
}
```

To this:

```
public async Task<EmailAddressEntity> TryReadAsync(Guid id)
{
	return await database
		.CreateQuery(@"
			SELECT *
			FROM ""email_address"" AS ea
			WHERE ea.id = :id")
		.SetParameter("id", id)
		.FirstOrDefaultAsync<EmailAddressEntity>();
}
```

