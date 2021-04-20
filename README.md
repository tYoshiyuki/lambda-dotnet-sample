# lambda-dotnet-sample
C# の AWS Lambda サンプル

## Feature
- C#
- .NET Core
- AWS Lambda
- Kinesis Data Streams

## Project
### LambdaSample.CommonLibrary
- Lambda関数用のベースクラス ライブラリです。

### LambdaSample.SampleEventAndResponseFunction
#### EventFunctionBase, IEventFunctionHandler
- イベント処理を行うLambda関数 と 関数のハンドラーインターフェースです。

#### EventAndResponseFunctionBase, IEventAndResponseFunctionHandler
- イベントを受け取り、レスポンスを返却するLambda関数 と 関数のハンドラーインターフェースです。

#### AbstractFunctionBase
- Lambda関数の共通処理を実装した抽象クラスです。
- .NET Core の DI機能、ロガー設定、JSON設定ファイルの機能を備えています。

### LambdaSample.PutDynamoDbEventFunction
- EventFunction の実装サンプルです。

### LambdaSample.SampleEventAndResponseFunction
- EventAndResponseFunction の実装サンプルです。

### LambdaFirehoseSample
- TODO

### LambdaKinesisSample
- TODO

### StepFunctionsSample
- TODO

## Note
- デバッグ実行には AWS Toolkit for Visual Studio の拡張機能が必要です。
- LambdaKinesisSample の実行には Kinesalite (https://github.com/mhart/kinesalite) を使用します。
- LambdaFirehoseSample の実行には LocalStack を使用します。
