using ChainingAssertion;
using LambdaSample.Services;
using Xunit;

namespace LambdaSample.Tests
{
    public class HelloServiceTest
    {
        [Fact]
        public void Greeting_正常系()
        {
            // Arrange
            var service = new HelloService();

            // Act
            var result = service.Greeting();

            // Assert
            result.Is("Hello World!");
        }
    }
}
