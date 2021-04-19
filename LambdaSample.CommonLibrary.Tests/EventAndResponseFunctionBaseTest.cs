using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace LambdaSample.CommonLibrary.Tests
{
    public class EventAndResponseFunctionBaseTest
    {
        [Test]
        public void EntryPoint_正常系()
        {
            // Arrange
            var function = new SampleEventAndResponseFunction();

            // Act
            var result = function.EntryPoint("hello world.", new TestLambdaContext());

            // Assert
            Assert.That(result, Is.EqualTo("HELLO WORLD."));
        }

        [Test]
        public void EntryPoint_異常系_FunctionHandler無し()
        {
            // Arrange
            var function = new SampleNoHandlerEventAndResponseFunction();

            // Act・Assert
            Assert.That(() => function.EntryPoint("hello world.", new TestLambdaContext()),
                Throws.Exception.TypeOf<InvalidOperationException>()
                    .And.Message.EqualTo("EntryPoint failed. FunctionHandler is nothing."));
        }

        [Test]
        public void EntryPoint_異常系_FunctionHandler実行時に例外が発生()
        {
            // Arrange
            var function = new SampleExceptionEventAndResponseFunction();

            // Act・Assert
            Assert.That(() => function.EntryPoint("hello world.", new TestLambdaContext()),
                Throws.Exception.TypeOf<Exception>()
                    .And.Message.EqualTo("This is test error."));
        }
    }

    /// <summary>
    /// テスト用のLambda関数クラスです。
    /// </summary>
    class SampleEventAndResponseFunction : EventAndResponseFunctionBase<string, string>
    {
        public SampleEventAndResponseFunction()
        {
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddSingleton<IEventAndResponseFunctionHandler<string, string>, SampleEventAndResponseFunctionHandler>();
        }
    }

    /// <summary>
    /// テスト用のFunctionHandlerクラスです。
    /// </summary>
    class SampleEventAndResponseFunctionHandler : IEventAndResponseFunctionHandler<string, string>
    {
        public string Handle(string input, ILambdaContext context)
        {
            // 入力文字列を大文字にしたものを返却します。
            return input.ToUpper();
        }
    }

    /// <summary>
    /// テスト用 (異常系・FunctionHandlerが未登録) のLambda関数クラスです。
    /// </summary>
    class SampleNoHandlerEventAndResponseFunction : EventAndResponseFunctionBase<string, string>
    {
        public SampleNoHandlerEventAndResponseFunction()
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
    class SampleExceptionEventAndResponseFunctionHandler : IEventAndResponseFunctionHandler<string, string>
    {
        public string Handle(string input, ILambdaContext context)
        {
            throw new Exception("This is test error.");
        }
    }

    /// <summary>
    /// テスト用 (異常系・FunctionHandlerの実行時に例外が発生) のLambda関数クラスです。
    /// </summary>
    class SampleExceptionEventAndResponseFunction : EventAndResponseFunctionBase<string, string>
    {
        public SampleExceptionEventAndResponseFunction()
        {
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddTransient<IEventAndResponseFunctionHandler<string, string>, SampleExceptionEventAndResponseFunctionHandler>();
        }
    }
}