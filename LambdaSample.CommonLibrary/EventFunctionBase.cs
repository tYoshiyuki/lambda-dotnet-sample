using System;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace LambdaSample.CommonLibrary
{
    /// <summary>
    /// イベント処理を行うLambda関数です。
    /// </summary>
    /// <typeparam name="T">イベントの型</typeparam>
    public abstract class EventFunctionBase<T> : AbstractFunctionBase
    {
        /// <summary>
        /// 関数のハンドラーです。エントリーポイントになります。
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="context">context</param>
        public void FunctionHandler(T input, ILambdaContext context)
        {
            LambdaLogger.Log("Start FunctionHandler.");

            using var scope = ServiceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<IEventFunctionHandlerCore<T>>();

            if (handler == null)
            {
                LambdaLogger.Log("FunctionHandler failed. FunctionHandler is nothing.");
                throw new InvalidOperationException("Handle failed. FunctionHandler is nothing.");
            }

            LambdaLogger.Log("Execute FunctionHandler.");
            LambdaLogger.Log("Context: " + JsonConvert.SerializeObject(context));
            LambdaLogger.Log("Input: " + JsonConvert.SerializeObject(input));

           handler.Handle(input, context);
        }
    }
}
