using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace LambdaSample
{
    public class Function
    {        
        /// <summary>
        /// “ü—Íƒpƒ‰ƒ[ƒ^‚ğ•Ô‹p‚·‚éŠÖ”‚Å‚·
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
