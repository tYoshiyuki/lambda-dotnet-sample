using LambdaSample.SampleEventAndResponseFunction.Services;
using NUnit.Framework;

namespace LambdaSample.SampleEventAndResponseFunction.Tests
{
    public class HelloServiceTest
    {
        [Test]
        public void Greeting_正常系()
        {
            // Arrange
            var service = new HelloService();

            // Act
            var result = service.Greeting();

            // Assert
            Assert.That(result, Is.EqualTo("Hello World!"));
        }
    }
}
