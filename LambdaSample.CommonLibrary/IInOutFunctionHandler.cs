using Amazon.Lambda.Core;

namespace LambdaSample.CommonLibrary
{
    public interface IInOutFunctionHandler<in TInput, out TOutput>
    {
        TOutput Handle(TInput input, ILambdaContext context);
    }
}
