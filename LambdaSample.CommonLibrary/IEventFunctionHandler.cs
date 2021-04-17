using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace LambdaSample.CommonLibrary
{
    /// <summary>
    /// イベント処理を行うLambda関数のハンドラーのインターフェースです。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventFunctionHandler<in T>
    {
        Task Handle(T input, ILambdaContext context);
    }
}
