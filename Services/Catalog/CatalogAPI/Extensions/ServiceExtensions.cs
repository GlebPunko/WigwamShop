using Catalog.Infastructure.Context;
using Confluent.Kafka;
using Hangfire;
using Hangfire.SqlServer;
using Kafka;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace CatalogAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigurationAppsettings(this IConfigurationBuilder configuration)
        {
            configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                 optional: false, reloadOnChange: true)
                .Build();
        }

        public static void ConfigureKafka(this IServiceCollection services)
        {
            services.AddKafkaConsumer<string, string, TestHandler>(broker =>
            {
                broker.Topic = "Catalog";
                broker.GroupId = "DemoGroup";
                broker.BootstrapServers = "broker:29092";
                broker.AutoOffsetReset = AutoOffsetReset.Earliest;
                broker.AllowAutoCreateTopics = true;
            });
            services.AddKafkaProducer<string, string>(producer =>
            {
                producer.Topic = "Basket";
                producer.BootstrapServers = "broker:29092";
            });
        }

        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configurations.GetConnectionString("HangfireConnection"),
            new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            services.AddHangfireServer(options =>
                options.SchedulePollingInterval = TimeSpan.FromSeconds(1));
        }

        public static void ConfigureLogging(IConfigurationRoot configuration)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                .CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{configuration["ELKConfiguration:IndexName"].ToLower()}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
