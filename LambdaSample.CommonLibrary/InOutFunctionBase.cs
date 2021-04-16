using System;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace LambdaSample.CommonLibrary
{
    /// <summary>
    /// イベントを受け取り、レスポンスを返却するLambda関数です。
    /// </summary>
    /// <typeparam name="TInput">イベントの型</typeparam>
    /// <typeparam name="TOutput">レスポンスの型</typeparam>
    public abstract class InOutFunctionBase<TInput, TOutput> : AbstractFunctionBase
    {
        /// <summary>
        /// 関数のハンドラーです。エントリーポイントになります。
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="context">context</param>
        /// <returns></returns>
        public TOutput FunctionHandler(TInput input, ILambdaContext context)
        {
            LambdaLogger.Log("Start FunctionHandler.");

            using var scope = ServiceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<IInOutFunctionHandlerCore<TInput, TOutput>>();

            if (handler == null)
            {
                LambdaLogger.Log("FunctionHandler failed. FunctionHandler is nothing.");
                throw new InvalidOperationException("Handle failed. FunctionHandler is nothing.");
            }

            LambdaLogger.Log("Execute FunctionHandler.");
            LambdaLogger.Log("Context: " + JsonConvert.SerializeObject(context));
            LambdaLogger.Log("Input: " + JsonConvert.SerializeObject(input));

            return handler.Handle(input, context);
        }
    }
}
