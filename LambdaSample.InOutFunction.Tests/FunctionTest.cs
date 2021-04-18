using Amazon.Lambda.TestUtilities;
using ChainingAssertion;
using LambdaSample.InOutFunction.Models;
using Xunit;

namespace LambdaSample.InOutFunction.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void FunctionHandler_正常系()
        {
            // Arrange
            var function = new SampleFunction();
            var context = new TestLambdaContext();

            // Act
            var result = function.EntryPoint(new SampleFunctionInput { Key1 = "hello world" }, context);

            // Assert
            result.Is("hello world And Hello World!");
        }
    }
}
