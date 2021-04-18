using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.SampleInOutFunction.Models;
using LambdaSample.SampleInOutFunction.Services;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaSample.SampleInOutFunction
{
    /// <summary>
    /// �T���v����Lambda�֐��ł��B
    /// </summary>
    public class SampleFunction : InOutFunctionBase<SampleFunctionInput, string>
    {
        public SampleFunction()
        {
            InitializeFunction();
        }

        /// <summary>
        /// �T�[�r�X�̐ݒ���s���܂��B
        /// </summary>
        /// <param name="services">services</param>
        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddSingleton<IHelloService, HelloService>();
            services.AddSingleton<IInOutFunctionHandler<SampleFunctionInput, string>, SampleInOutFunctionHandler>();
        }
    }
}
