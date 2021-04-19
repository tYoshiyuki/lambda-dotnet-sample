using Amazon.Lambda.Core;

namespace LambdaSample.CommonLibrary
{
    /// <summary>
    /// イベントを受け取り、レスポンスを返却するLambda関数のハンドラーのインターフェースです。
    /// </summary>
    /// <typeparam name="TInput">イベントの型</typeparam>
    /// <typeparam name="TOutput">レスポンスの型</typeparam>
    public interface IEventAndResponseFunctionHandler<in TInput, out TOutput>
    {
        TOutput Handle(TInput input, ILambdaContext context);
    }
}
