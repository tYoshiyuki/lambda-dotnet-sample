using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace LambdaSample.CommonLibrary.Tests
{
    public class EventFunctionBaseTest
    {
        [Test]
        public void EntryPoint_正常系()
        {
            // Arrange
            var function = new SampleEventFunction();

            // Act
            function.EntryPoint("hello world.", new TestLambdaContext());

            // Assert
            Assert.That(SampleEventFunction.Counter, Is.EqualTo(1));
        }

        [Test]
        public void EntryPoint_異常系_FunctionHandler無し()
        {
            // Arrange
            var function = new SampleNoHandlerEventFunction();

            // Act・Assert
            Assert.That(() => function.EntryPoint("hello world.", new TestLambdaContext()),
                Throws.Exception.TypeOf<InvalidOperationException>()
                    .And.Message.EqualTo("EntryPoint failed. FunctionHandler is nothing."));
        }
    }

    /// <summary>
    /// テスト用のLambda関数クラスです。
    /// </summary>
    class SampleEventFunction : EventFunctionBase<string>
    {
        public static int Counter { get; set; }


        public SampleEventFunction()
        {
            Counter = 0;
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddTransient<IEventFunctionHandler<string>, SampleEventFunctionHandler>();
        }
    }

    /// <summary>
    /// テスト用のFunctionHandlerクラスです。
    /// </summary>
    class SampleEventFunctionHandler : IEventFunctionHandler<string>
    {
        public void Handle(string input, ILambdaContext context)
        {
            SampleEventFunction.Counter++;
        }
    }

    /// <summary>
    /// テスト用 (異常系) のLambda関数クラスです。
    /// </summary>
    class SampleNoHandlerEventFunction : EventFunctionBase<string>
    {
        public SampleNoHandlerEventFunction()
        {
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services) { }
    }
}
