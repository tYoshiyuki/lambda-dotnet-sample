using Amazon.Lambda.TestUtilities;
using LambdaSample.SampleInOutFunction.Models;
using LambdaSample.SampleInOutFunction.Services;
using Moq;
using NUnit.Framework;

namespace LambdaSample.SampleInOutFunction.Tests
{
    public class SampleInOutFunctionHandlerTest
    {
        [Test]
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
            Assert.That(result, Is.EqualTo("hello world And Hello Mock!"));
        }
    }
}
