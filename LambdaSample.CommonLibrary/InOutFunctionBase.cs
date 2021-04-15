using System;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace LambdaSample.CommonLibrary
{
    public abstract class InOutFunctionBase<TInput, TOutput> : AbstractFunctionBase
    {
        public TOutput FunctionHandler(TInput input, ILambdaContext context)
        {
            LambdaLogger.Log("Start FunctionHandler.");

            using var scope = ServiceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<IInOutFunctionHandler<TInput, TOutput>>();

            if (handler == null)
            {
                LambdaLogger.Log("Handle failed. FunctionHandler is nothing.");
                throw new InvalidOperationException("Handle failed. FunctionHandler is nothing.");
            }

            LambdaLogger.Log("Execute handle.");
            LambdaLogger.Log("Context: " + JsonConvert.SerializeObject(context));
            LambdaLogger.Log("Input: " + JsonConvert.SerializeObject(input));

            return handler.Handle(input, context);
        }
    }
}
