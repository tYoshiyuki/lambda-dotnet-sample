using Amazon.Lambda.TestUtilities;
using ChainingAssertion;
using LambdaSample.InOutFunction.Models;
using LambdaSample.InOutFunction.Services;
using Moq;
using Xunit;

namespace LambdaSample.InOutFunction.Tests
{
    public class SampleInOutFunctionHandlerCoreTest
    {
        [Fact]
        public void Handle_正常系()
        {
            // Arrange
            var mock = new Mock<IHelloService>();
            mock.Setup(x => x.Greeting()).Returns("Hello Mock!");
            var handler = new SampleInOutFunctionHandler(mock.Object);
            var context = new TestLambdaContext();

            // Act
            var result = handler.Handle(new SampleFunctionInput {Key1 = "hello world"}, context);

            // Assert
            result.Is("hello world And Hello Mock!");
        }
    }
}
