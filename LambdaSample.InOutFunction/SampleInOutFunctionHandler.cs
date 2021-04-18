using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.InOutFunction.Models;
using LambdaSample.InOutFunction.Services;

namespace LambdaSample.InOutFunction
{
    /// <summary>
    /// サンプルのLambda関数のハンドラーです。
    /// </summary>
    public class SampleInOutFunctionHandler : IInOutFunctionHandler<SampleFunctionInput, string>
    {
        private readonly IHelloService _service;
        public SampleInOutFunctionHandler(IHelloService service)
        {
            _service = service;
        }

        public string Handle(SampleFunctionInput input, ILambdaContext context)
        {
            // TODO ビジネスロジックを呼び出す場合は、コンストラクタインジェクションにより取得してください。
            return $"{input?.Key1} And {_service.Greeting()}";
        }
    }
}
