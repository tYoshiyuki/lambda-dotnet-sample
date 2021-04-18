using System;
using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.PutDynamoDbEventFunction.Models;
using LambdaSample.PutDynamoDbEventFunction.Services;
using Newtonsoft.Json;

namespace LambdaSample.PutDynamoDbEventFunction
{
    public class PutDynamoDbEventFunctionHandler: IEventFunctionHandler<PutDynamoDbEventFunctionInput>
    {
        private readonly ISampleUserService service;
        public PutDynamoDbEventFunctionHandler(ISampleUserService service)
        {
            this.service = service;
        }

        public void Handle(PutDynamoDbEventFunctionInput input, ILambdaContext context)
        {
            var id = DateTime.Now.ToString("yyyyMMddHHmmss");
            var user = new SampleUser { Id = id, Name = $"sample-user-{id}" };
            service.Create(user);
            var result= service.Get(user.Id);
            LambdaLogger.Log("Result: " + JsonConvert.SerializeObject(result));
        }
    }
}
