using Amazon.Lambda.TestUtilities;
using LambdaSample.SampleEventAndResponseFunction.Models;
using NUnit.Framework;

namespace LambdaSample.SampleEventAndResponseFunction.Tests
{
    public class FunctionTest
    {
        [Test]
        public void FunctionHandler_正常系()
        {
            // Arrange
            var function = new SampleEventAndResponseFunction();
            var context = new TestLambdaContext();

            // Act
            var result = function.EntryPoint(new SampleEventAndResponseFunctionInput { Key1 = "hello world" }, context);

            // Assert
            Assert.That(result, Is.EqualTo("hello world And Hello World!"));
        }
    }
}
