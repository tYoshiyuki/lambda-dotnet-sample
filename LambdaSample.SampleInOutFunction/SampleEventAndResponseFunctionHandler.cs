using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.SampleInOutFunction.Models;
using LambdaSample.SampleInOutFunction.Services;

namespace LambdaSample.SampleInOutFunction
{
    /// <summary>
    /// サンプルのLambda関数のハンドラーです。
    /// </summary>
    public class SampleEventAndResponseFunctionHandler : IEventAndResponseFunctionHandler<SampleInOutFunctionInput, string>
    {
        private readonly IHelloService _service;
        public SampleEventAndResponseFunctionHandler(IHelloService service)
        {
            _service = service;
        }

        public string Handle(SampleInOutFunctionInput input, ILambdaContext context)
        {
            // TODO ビジネスロジックを呼び出す場合は、コンストラクタインジェクションにより取得してください。
            return $"{input?.Key1} And {_service.Greeting()}";
        }
    }
}
