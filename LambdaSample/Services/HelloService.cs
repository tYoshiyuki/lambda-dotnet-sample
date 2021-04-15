using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaSample.Services
{
    public interface IHelloService
    {
        string Greeting();
    }

    public class HelloService : IHelloService
    {
        public string Greeting()
        {
            return "Hello World!";
        }
    }
}
