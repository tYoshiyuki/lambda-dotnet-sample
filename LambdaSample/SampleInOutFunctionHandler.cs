using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.Models;
using LambdaSample.Services;

namespace LambdaSample
{
    public class SampleInOutFunctionHandler : IInOutFunctionHandler<FunctionInput, string>
    {
        private readonly IHelloService _service;
        public SampleInOutFunctionHandler(IHelloService service)
        {
            _service = service;
        }

        public string Handle(FunctionInput input, ILambdaContext context)
        {
            LambdaLogger.Log(_service.Greeting());
            return input?.Key1;
        }
    }
}
