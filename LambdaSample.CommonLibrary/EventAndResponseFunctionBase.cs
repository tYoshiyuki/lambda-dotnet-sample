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
    public abstract class EventAndResponseFunctionBase<TInput, TOutput> : AbstractFunctionBase
    {
        /// <summary>
        /// 関数のエントリーポイントです。
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="context">context</param>
        /// <returns></returns>
        public TOutput EntryPoint(TInput input, ILambdaContext context)
        {
            LambdaLogger.Log("Start EntryPoint.");

            using var scope = ServiceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<IEventAndResponseFunctionHandler<TInput, TOutput>>();

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
                return handler.Handle(input, context);
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
