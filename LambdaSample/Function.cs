using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.Models;
using LambdaSample.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace LambdaSample
{
    public class Function : InOutFunctionBase<FunctionInput, string>
    {
        public Function()
        {
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddSingleton<IHelloService, HelloService>();
            services.AddSingleton<IInOutFunctionHandler<FunctionInput, string>, SampleInOutFunctionHandler>();
        }
    }
}
