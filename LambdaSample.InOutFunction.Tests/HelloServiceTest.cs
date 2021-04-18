using ChainingAssertion;
using LambdaSample.InOutFunction.Services;
using Xunit;

namespace LambdaSample.InOutFunction.Tests
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
