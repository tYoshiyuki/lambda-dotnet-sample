using Amazon.DynamoDBv2.DataModel;

namespace LambdaSample.PutDynamoDbEventFunction.Models
{
    [DynamoDBTable("SampleUser")]
    public class SampleUser
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }
    }
}
