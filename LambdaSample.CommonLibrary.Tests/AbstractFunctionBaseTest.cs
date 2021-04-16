using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace LambdaSample.CommonLibrary.Tests
{
    public class AbstractFunctionBaseTest
    {
        [Test]
        public void InitializeFunction_正常系()
        {
            // Arrange
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "env1");
            var target = new SampleAbstractFunctionBase();

            // Act
            target.Initialize();

            // Assert
            var config = target.GetConfiguration();
            Assert.That(config["SampleKey"], Is.EqualTo("SampleValue"));
            Assert.That(config["SampleEnvKey"], Is.EqualTo("SampleEnvValue1"));

            var sample = target.GetServiceProvider().GetService<Sample>();
            Assert.That(sample, Is.Not.Null);
        }
    }

    class SampleAbstractFunctionBase: AbstractFunctionBase
    {
        public void Initialize()
        {
            InitializeFunction();
        }

        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddTransient<Sample>();
        }

        public IServiceProvider GetServiceProvider()
        {
            return ServiceProvider;
        }

        public IConfiguration GetConfiguration()
        {
            return Configuration;
        }
    }

    class Sample { }
}
