using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LambdaSample.CommonLibrary
{
    /// <summary>
    /// AbstractFunctionBase です。
    /// </summary>
    public abstract class AbstractFunctionBase
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected IConfiguration Configuration { get; set; }

        /// <summary>
        /// Lambda関数の初期処理を行います。
        /// </summary>
        protected void InitializeFunction()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton(Configuration);
            services.AddLogging(ConfigureLogger);
            ConfigureService(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        public static void ConfigureLogger(ILoggingBuilder logging)
        {
            if (logging == null)
            {
                throw new ArgumentNullException(nameof(logging));
            }

            var loggerOptions = new LambdaLoggerOptions
            {
                IncludeCategory = true,
                IncludeLogLevel = true,
                IncludeNewline = true,
                IncludeEventId = true,
                IncludeException = true
            };

            logging.AddLambdaLogger(loggerOptions);
        }

        protected abstract void ConfigureService(IServiceCollection services);
    }
}
