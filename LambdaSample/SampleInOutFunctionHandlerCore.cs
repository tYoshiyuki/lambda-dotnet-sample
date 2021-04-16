using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.Models;
using LambdaSample.Services;

namespace LambdaSample
{
    /// <summary>
    /// サンプルのLambda関数のハンドラーです。
    /// </summary>
    public class SampleInOutFunctionHandlerCore : IInOutFunctionHandlerCore<FunctionInput, string>
    {
        private readonly IHelloService _service;
        public SampleInOutFunctionHandlerCore(IHelloService service)
        {
            _service = service;
        }

        public string Handle(FunctionInput input, ILambdaContext context)
        {
            // TODO ビジネスロジックを呼び出す場合は、コンストラクタインジェクションにより取得してください。
            return $"{input?.Key1} And {_service.Greeting()}";
        }
    }
}
