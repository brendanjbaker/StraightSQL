using SimpleInjector;
using SimpleInjector.Advanced;
using StraightSql;
using StraightSql.Composition.SimpleInjector;
using StraightSql.Conversion;
using StraightSql.Conversion.Source;
using StraightSql.Entity;
using System;
using System.Linq;

public static class StraightSqlServiceCollectionExtensions
{
	public static void RegisterEntity<TEntity>(this Container container, String tableName, Action<IEntityRegistrationOptionsBuilder<TEntity>> configure)
		where TEntity : new()
	{
		var registrationBuilder = new EntityRegistrationOptionsBuilder<TEntity>(tableName);

		configure(registrationBuilder);

		var registration = registrationBuilder.Build();

		container.AppendToCollection(
			typeof(IEntityRegistration),
			Lifestyle.Singleton.CreateRegistration(() => registration, container));
	}

	public static void RegisterStraightSql(this Container container, Action<StraightSqlOptions> configure)
	{
		var options = new StraightSqlOptions();

		configure(options);

		if (options.ConnectionStringFactory == null)
			throw new Exception("No connection string factory defined.");

		container.RegisterSingleton<ICommandPreparer, CommandPreparer>();
		container.RegisterSingleton<IDatabase, Database>();
		container.RegisterSingleton<IEntityContext, EntityContext>();
		container.RegisterSingleton<IQueryDispatcher, QueryDispatcher>();
		container.RegisterCollection<IEntityRegistration>();

		container.RegisterSingleton<IConnectionFactory>(() =>
		{
			var connectionString = options.ConnectionStringFactory(container);

			return new ConnectionFactory(connectionString);
		});

		container.RegisterSingleton<ITypeConverter>(() =>
		{
			return
				new SourcedTypeConverter(
					new CachingTypeConverterSource(
						new CascadingTypeConverterSource(
							new ValueTypeConverterSource(),
							new NullableTypeConverterSource(),
							new UserDefinedTypeConverterSource(options.TypeConverters))));
		});

		container.RegisterSingleton<IQueryExecutor, QueryExecutor>();
	}
}
