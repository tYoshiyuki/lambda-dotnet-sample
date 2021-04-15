using System;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace LambdaSample.CommonLibrary
{
    public abstract class EventFunctionBase<T> : AbstractFunctionBase
    {
        public void FunctionHandler(T input, ILambdaContext context)
        {
            LambdaLogger.Log("Start FunctionHandler.");

            using var scope = ServiceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<IEventFunctionHandler<T>>();

            if (handler == null)
            {
                LambdaLogger.Log("Handle failed. FunctionHandler is nothing.");
                throw new InvalidOperationException("Handle failed. FunctionHandler is nothing.");
            }

            LambdaLogger.Log("Execute handle.");
            LambdaLogger.Log("Context: " + JsonConvert.SerializeObject(context));
            LambdaLogger.Log("Input: " + JsonConvert.SerializeObject(input));

           handler.Handle(input, context);
        }
    }
}
