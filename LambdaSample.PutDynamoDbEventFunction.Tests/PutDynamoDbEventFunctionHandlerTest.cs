using Amazon.Lambda.TestUtilities;
using LambdaSample.PutDynamoDbEventFunction.Models;
using LambdaSample.PutDynamoDbEventFunction.Services;
using Moq;
using NUnit.Framework;

namespace LambdaSample.PutDynamoDbEventFunction.Tests
{
    public class PutDynamoDbEventFunctionHandlerTest
    {
        [Test]
        public void Handle_正常系()
        {
            // Arrange
            var mock = new Mock<ISampleUserService>();
            mock.Setup(x => x.Create(It.IsAny<SampleUser>()));
            var handler = new PutDynamoDbEventFunctionHandler(mock.Object);
            var context = new TestLambdaContext();

            // Act
            handler.Handle(new PutDynamoDbEventFunctionInput { Key1 = "hello world" }, context);

            // Assert
            mock.Verify(x => x.Create(It.IsAny<SampleUser>()), Times.Once());
        }
    }
}
