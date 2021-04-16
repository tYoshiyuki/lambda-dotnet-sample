using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.Models;
using LambdaSample.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace LambdaSample
{
    /// <summary>
    /// サンプルのLambda関数です。
    /// </summary>
    public class Function : InOutFunctionBase<FunctionInput, string>
    {
        public Function()
        {
            InitializeFunction();
        }

        /// <summary>
        /// サービスの設定を行います。
        /// </summary>
        /// <param name="services">services</param>
        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddSingleton<IHelloService, HelloService>();
            services.AddSingleton<IInOutFunctionHandlerCore<FunctionInput, string>, SampleInOutFunctionHandlerCore>();
        }
    }
}
