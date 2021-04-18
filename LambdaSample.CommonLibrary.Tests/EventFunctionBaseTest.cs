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

        [Test]
        public void EntryPoint_異常系_FunctionHandler実行時に例外が発生()
        {
            // Arrange
            var function = new SampleExceptionEventFunction();

            // Act・Assert
            Assert.That(() => function.EntryPoint("hello world.", new TestLambdaContext()),
                Throws.Exception.TypeOf<Exception>()
                    .And.Message.EqualTo("This is test error."));
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
    /// テスト用 (異常系・FunctionHandlerが未登録) のLambda関数クラスです。
    /// </summary>
    class SampleNoHandlerEventFunction : EventFunctionBase<string>
    {
        public SampleNoHandlerEventFunction()
        {
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services)
        {
        }
    }

    /// <summary>
    /// テスト用 (異常系) のFunctionHandlerクラスです。
    /// </summary>
    class SampleExceptionEventFunctionHandler : IEventFunctionHandler<string>
    {
        public void Handle(string input, ILambdaContext context)
        {
            throw new Exception("This is test error.");
        }
    }

    /// <summary>
    /// テスト用 (異常系・FunctionHandlerの実行時に例外が発生) のLambda関数クラスです。
    /// </summary>
    class SampleExceptionEventFunction : EventFunctionBase<string>
    {
        public SampleExceptionEventFunction()
        {
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddTransient<IEventFunctionHandler<string>, SampleExceptionEventFunctionHandler>();
        }
    }
}