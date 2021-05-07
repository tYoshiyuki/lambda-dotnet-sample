using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaKinesisSample
{
    public class Function
    {
        private readonly string _streamName = "sample-kinesis-stream";

        // https://github.com/mhart/kinesalite
        private readonly string _serviceURL = "http://localhost:4567/";

        /// <summary>
        /// Kinesis Stream からイベントを受け取り、Kinesis Streamへデータを書き込む関数です
        /// ローカル開発環境として Kinesalite を使用しています
        /// </summary>
        /// <param name="kinesisEvent"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(KinesisEvent kinesisEvent, ILambdaContext context)
        {
            context.Logger.LogLine($"Beginning to process {kinesisEvent.Records.Count} records...");

            // Kinesis Eventからレコードを取得し、ループ処理を行う
            foreach (var record in kinesisEvent.Records)
            {
                context.Logger.LogLine($"Event ID: {record.EventId}");
                context.Logger.LogLine($"Event Name: {record.EventName}");

                var recordData = GetRecordContents(record.Kinesis);
                context.Logger.LogLine($"Record Data:");
                context.Logger.LogLine(recordData);
            }

            #region ローカル開発用のコード
            // Kinesis Stream の作成, 通常は AWSコンソール より作成する
            var request = new CreateStreamRequest
            {
                ShardCount = 1,
                StreamName = _streamName
            };

            var client = new AmazonKinesisClient(new AmazonKinesisConfig
            {
                ServiceURL = _serviceURL
            });

            var response = await client.ListStreamsAsync();
            if (!response.StreamNames.Any(_ => _ == _streamName))
            {
                await client.CreateStreamAsync(request);
            }
            #endregion

            // Kinesis Stream に対してデータを書き込む
            foreach (var i in Enumerable.Range(1, 10))
            {
                using (var memory = new MemoryStream(Encoding.UTF8.GetBytes($"Put Data:{i}")))
                {
                    try
                    {
                        var req = new PutRecordRequest
                        {
                            StreamName = _streamName,
                            PartitionKey = "url-response-times",
                            Data = memory
                        };

                        var res = await client.PutRecordAsync(req);
                        context.Logger.LogLine($"Successfully sent record to Kinesis. Sequence number: {res.SequenceNumber}.");
                    }
                    catch (Exception ex)
                    {
                        context.Logger.LogLine($"Failed to send record to Kinesis. Exception: {ex.Message}.");
                    }
                }
                context.Logger.LogLine("Stream processing complete.");
            }
        }

        private string GetRecordContents(KinesisEvent.Record streamRecord)
        {
            using (var reader = new StreamReader(streamRecord.Data, Encoding.ASCII))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
