using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.KinesisFirehose;
using Amazon.KinesisFirehose.Model;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaFirehoseSample
{
    public class Function
    {
        private readonly string _bucket = "sample-bucket";
        private readonly string _deliveryStream = "sample-delivery-stream";
        private readonly string _serviceURL = "http://localhost:4573";

        /// <summary>
        /// Kinesis Firehoseへデータを書き込む関数です
        /// ローカル開発環境として localStack を使用しています
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(FunctionInput input, ILambdaContext context)
        {
            #region ローカル開発用のコード
            // S3 の作成
            var creds = new BasicAWSCredentials("dummy", "dummy");
            var s3Client = new AmazonS3Client(creds, new AmazonS3Config
            {
                ServiceURL = "http://localhost:4572",
                UseHttp = true,
                ForcePathStyle = true,
                AuthenticationRegion = "us-east-1",
            });

            if (!await AmazonS3Util.DoesS3BucketExistAsync(s3Client, _bucket))
            {
                await s3Client.PutBucketAsync(new PutBucketRequest
                {
                    BucketName = _bucket,
                    UseClientRegion = true
                });
            }

            // Kinesis Firehose の作成
            var request = new CreateDeliveryStreamRequest
            {
                DeliveryStreamName = _deliveryStream,
                ExtendedS3DestinationConfiguration = new ExtendedS3DestinationConfiguration
                {
                    BucketARN = $"arn:aws:s3:::{_bucket}",
                    Prefix = "firehose/",
                    RoleARN = "arn:aws:iam::dummy:role/dummy",
                }
            };

            var client = new AmazonKinesisFirehoseClient(creds, new AmazonKinesisFirehoseConfig
            {
                ServiceURL = _serviceURL,
            });

            var response = await client.ListDeliveryStreamsAsync();
            if (!response.DeliveryStreamNames.Any(_ => _ == _deliveryStream))
            {
                await client.CreateDeliveryStreamAsync(request);
            }
            #endregion

            var data = JsonConvert.SerializeObject(input);
            var record = new Record()
            {
                Data = new MemoryStream(Encoding.UTF8.GetBytes(data))
            };

            // Kinesis Firehose に対してデータを書き込む
            var res = await client.PutRecordAsync(_deliveryStream, record);
            if (res.HttpStatusCode == HttpStatusCode.OK)
            {
                return $"RecordId: {res.RecordId}";
            }
            else
            {
                return "Error";
            }
        }
    }

    public class FunctionInput
    {
        public string Key1 { get; set; }
    }
}
