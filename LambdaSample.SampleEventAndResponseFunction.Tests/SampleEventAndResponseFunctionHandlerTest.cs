using Amazon.Lambda.TestUtilities;
using LambdaSample.SampleEventAndResponseFunction.Models;
using LambdaSample.SampleEventAndResponseFunction.Services;
using Moq;
using NUnit.Framework;

namespace LambdaSample.SampleEventAndResponseFunction.Tests
{
    public class SampleEventAndResponseFunctionHandlerTest
    {
        [Test]
        public void Handle_正常系()
        {
            // Arrange
            var mock = new Mock<IHelloService>();
            mock.Setup(x => x.Greeting()).Returns("Hello Mock!");
            var handler = new SampleEventAndResponseFunctionHandler(mock.Object);
            var context = new TestLambdaContext();

            // Act
            var result = handler.Handle(new SampleEventAndResponseFunctionInput {Key1 = "hello world"}, context);

            // Assert
            Assert.That(result, Is.EqualTo("hello world And Hello Mock!"));
        }
    }
}
