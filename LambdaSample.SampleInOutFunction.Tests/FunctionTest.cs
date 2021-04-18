using Amazon.Lambda.TestUtilities;
using LambdaSample.SampleInOutFunction.Models;
using NUnit.Framework;

namespace LambdaSample.SampleInOutFunction.Tests
{
    public class FunctionTest
    {
        [Test]
        public void FunctionHandler_正常系()
        {
            // Arrange
            var function = new SampleInOutFunction();
            var context = new TestLambdaContext();

            // Act
            var result = function.EntryPoint(new SampleInOutFunctionInput { Key1 = "hello world" }, context);

            // Assert
            Assert.That(result, Is.EqualTo("hello world And Hello World!"));
        }
    }
}
