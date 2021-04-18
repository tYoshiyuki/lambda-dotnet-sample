using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace LambdaSample.CommonLibrary.Tests
{
    public class InOutFunctionBaseTest
    {
        [Test]
        public void EntryPoint_正常系()
        {
            // Arrange
            var function = new SampleInOutFunction();

            // Act
            var result = function.EntryPoint("hello world.", new TestLambdaContext());

            // Assert
            Assert.That(result, Is.EqualTo("HELLO WORLD."));
        }

        [Test]
        public void EntryPoint_異常系_FunctionHandler無し()
        {
            // Arrange
            var function = new SampleNoHandlerInOutFunction();

            // Act・Assert
            Assert.That(() => function.EntryPoint("hello world.", new TestLambdaContext()),
                Throws.Exception.TypeOf<InvalidOperationException>()
                    .And.Message.EqualTo("EntryPoint failed. FunctionHandler is nothing."));
        }

        [Test]
        public void EntryPoint_異常系_FunctionHandler実行時に例外が発生()
        {
            // Arrange
            var function = new SampleExceptionInOutFunction();

            // Act・Assert
            Assert.That(() => function.EntryPoint("hello world.", new TestLambdaContext()),
                Throws.Exception.TypeOf<Exception>()
                    .And.Message.EqualTo("This is test error."));
        }
    }

    /// <summary>
    /// テスト用のLambda関数クラスです。
    /// </summary>
    class SampleInOutFunction : InOutFunctionBase<string, string>
    {
        public SampleInOutFunction()
        {
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddSingleton<IInOutFunctionHandler<string, string>, SampleInOutFunctionHandler>();
        }
    }

    /// <summary>
    /// テスト用のFunctionHandlerクラスです。
    /// </summary>
    class SampleInOutFunctionHandler : IInOutFunctionHandler<string, string>
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
    class SampleNoHandlerInOutFunction : InOutFunctionBase<string, string>
    {
        public SampleNoHandlerInOutFunction()
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
    class SampleExceptionInOutFunctionHandler : IInOutFunctionHandler<string, string>
    {
        public string Handle(string input, ILambdaContext context)
        {
            throw new Exception("This is test error.");
        }
    }

    /// <summary>
    /// テスト用 (異常系・FunctionHandlerの実行時に例外が発生) のLambda関数クラスです。
    /// </summary>
    class SampleExceptionInOutFunction : InOutFunctionBase<string, string>
    {
        public SampleExceptionInOutFunction()
        {
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddTransient<IInOutFunctionHandler<string, string>, SampleExceptionInOutFunctionHandler>();
        }
    }
}