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
        /// 関数のエントリーポイントです。
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="context">context</param>
        public void EntryPoint(T input, ILambdaContext context)
        {
            LambdaLogger.Log("Start EntryPoint.");

            using var scope = ServiceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<IEventFunctionHandler<T>>();

            if (handler == null)
            {
                LambdaLogger.Log("EntryPoint failed. FunctionHandler is nothing.");
                throw new InvalidOperationException("EntryPoint failed. FunctionHandler is nothing.");
            }

            LambdaLogger.Log("Execute EntryPoint.");
            LambdaLogger.Log("Context: " + JsonConvert.SerializeObject(context));
            LambdaLogger.Log("Input: " + JsonConvert.SerializeObject(input));

            try
            {
                handler.Handle(input, context);
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("EntryPoint failed.");
                LambdaLogger.Log(ex.Message);
                LambdaLogger.Log(ex.StackTrace);
                throw;
            }
        }
    }
}
