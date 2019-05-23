using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace LambdaSample
{
    public class Function
    {        
        /// <summary>
        /// 入力パラメータを返却する関数です
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(FunctionInput input, ILambdaContext context)
        {
            return input?.Key1;
        }
    }
}
