using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.Models;
using LambdaSample.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace LambdaSample
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
