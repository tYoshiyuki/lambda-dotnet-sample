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
        /// 各継承先の関数コンストラクタで呼び出してください。
        /// </summary>
        protected void InitializeFunction()
        {
            // 環境変数に応じて、設定ファイルの読み込みを行います。
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // DIコンテナの初期設定を行います。
            var services = new ServiceCollection();
            services.AddSingleton(Configuration);
            services.AddLogging(ConfigureLogger);

            ConfigureService(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// ロガーの設定を行います。
        /// </summary>
        /// <param name="logging">logging</param>
        protected void ConfigureLogger(ILoggingBuilder logging)
        {
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

        /// <summary>
        /// サービスの設定を行います。
        /// 各関数で必要なクラスは、ここで登録してください。
        /// </summary>
        /// <param name="services">services</param>
        protected abstract void ConfigureService(IServiceCollection services);
    }
}
