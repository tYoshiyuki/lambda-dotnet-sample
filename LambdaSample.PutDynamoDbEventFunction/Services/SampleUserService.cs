using Amazon.DynamoDBv2.DataModel;
using LambdaSample.PutDynamoDbEventFunction.Models;

namespace LambdaSample.PutDynamoDbEventFunction.Services
{
    public interface ISampleUserService
    {
        void Create(SampleUser user);

        SampleUser Get(string id);
    }

    public class SampleUserService : ISampleUserService
    {
        private readonly IDynamoDBContext dbContext;

        public SampleUserService(IDynamoDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(SampleUser user)
        {
            dbContext.SaveAsync(user).Wait();
        }

        public SampleUser Get(string id)
        {
            return dbContext.LoadAsync<SampleUser>(id).Result;
        }
    }
}
