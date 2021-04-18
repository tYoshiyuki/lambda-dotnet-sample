using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using LambdaSample.CommonLibrary;
using LambdaSample.PutDynamoDbEventFunction.Models;
using LambdaSample.PutDynamoDbEventFunction.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaSample.PutDynamoDbEventFunction
{
    public class PutDynamoDbEventFunction : EventFunctionBase<PutDynamoDbEventFunctionInput>
    {
        public PutDynamoDbEventFunction()
        {
            InitializeFunction();
        }
        
        protected override void ConfigureService(IServiceCollection services)
        {
            var dynamoDbConfig = Configuration.GetSection("DynamoDb");
            var runLocalDynamoDb = dynamoDbConfig.GetValue<bool>("LocalMode");

            if (runLocalDynamoDb)
            {
                services.AddSingleton<IAmazonDynamoDB>(sp =>
                {
                    var clientConfig = new AmazonDynamoDBConfig { ServiceURL = dynamoDbConfig.GetValue<string>("LocalServiceUrl") };
                    return new AmazonDynamoDBClient(clientConfig);
                });
            }
            else
            {
                services.AddAWSService<IAmazonDynamoDB>();
            }

            services.AddSingleton<IEventFunctionHandler<PutDynamoDbEventFunctionInput>, PutDynamoDbEventFunctionHandler>();
            services.AddSingleton<IDynamoDBContext>(x => new DynamoDBContext(x.GetRequiredService<IAmazonDynamoDB>()));
            services.AddSingleton<ISampleUserService, SampleUserService>();
        }
    }
}
