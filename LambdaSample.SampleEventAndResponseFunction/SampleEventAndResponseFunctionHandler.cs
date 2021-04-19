using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.SampleEventAndResponseFunction.Models;
using LambdaSample.SampleEventAndResponseFunction.Services;

namespace LambdaSample.SampleEventAndResponseFunction
{
    /// <summary>
    /// サンプルのLambda関数のハンドラーです。
    /// </summary>
    public class SampleEventAndResponseFunctionHandler : IEventAndResponseFunctionHandler<SampleEventAndResponseFunctionInput, string>
    {
        private readonly IHelloService _service;
        public SampleEventAndResponseFunctionHandler(IHelloService service)
        {
            _service = service;
        }

        public string Handle(SampleEventAndResponseFunctionInput input, ILambdaContext context)
        {
            // TODO ビジネスロジックを呼び出す場合は、コンストラクタインジェクションにより取得してください。
            return $"{input?.Key1} And {_service.Greeting()}";
        }
    }
}
