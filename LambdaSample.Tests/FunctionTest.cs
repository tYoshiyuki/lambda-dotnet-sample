using Amazon.Lambda.TestUtilities;
using ChainingAssertion;
using LambdaSample.Models;
using Xunit;

namespace LambdaSample.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void FunctionHandler_ê≥èÌån()
        {
            // Arrange
            var function = new Function();
            var context = new TestLambdaContext();

            // Act
            var result = function.FunctionHandler(new FunctionInput { Key1 = "hello world" }, context);

            // Assert
            result.Is("hello world And Hello World!");
        }
    }
}
