using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace LambdaSample.CommonLibrary
{
    public interface IEventFunctionHandler<in T>
    {
        Task Handle(T input, ILambdaContext context);
    }
}
