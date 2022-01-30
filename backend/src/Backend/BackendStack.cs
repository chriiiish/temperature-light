using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Lambda.EventSources;
using Amazon.CDK.AWS.SQS;
using Constructs;

namespace Backend
{
    public class BackendStack : Stack
    {
        internal BackendStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            /* 
             * Database
             */
            var devicesTable = new Table(this, "dynamo-devices", new TableProps{
                PartitionKey = new Attribute{ 
                    Name = "device_id",
                    Type = AttributeType.STRING
                },
                SortKey = new Attribute{
                    Name = "latest_reading_iso_timestamp",
                    Type = AttributeType.STRING
                },
                TableName = "temperature-light-devices",
                Encryption = TableEncryption.AWS_MANAGED,
                BillingMode = BillingMode.PAY_PER_REQUEST
            });

            var temperatureHistoryTable = new Table(this, "dynamo-history", new TableProps{
                PartitionKey = new Attribute{ 
                    Name = "device_id",
                    Type = AttributeType.STRING
                },
                SortKey = new Attribute{
                    Name = "iso_timestamp",
                    Type = AttributeType.STRING
                },
                TableName = "temperature-history",
                Encryption = TableEncryption.AWS_MANAGED,
                BillingMode = BillingMode.PAY_PER_REQUEST
            });


            /*
             * Devices Data Writer
             */
            var devicesSqsQueue = new Queue(this, "devices-sqs", new QueueProps{
                QueueName = "devices-datawriter",
                Encryption  = QueueEncryption.KMS
            });

            var devicesDataWriter = new Function(this, "devices-datawriter", new FunctionProps{
                FunctionName = "devices-datawriter",
                Code = Code.FromAsset("src/Lambdas"),
                Runtime = Runtime.DOTNET_CORE_3_1,
                Handler = "Lambdas::Lambda.DeviceDataWriter::Handle"
            });
            devicesDataWriter.AddEventSource(new SqsEventSource(devicesSqsQueue));


            /*
             * Temperature Data Writer
             */
            var temperatureSqsQueue = new Queue(this, "temperature-sqs", new QueueProps{
                QueueName = "temperature-datawriter",
                Encryption  = QueueEncryption.KMS
            });

            var temperatureDataWriter = new Function(this, "temperature-datawriter", new FunctionProps{
                FunctionName = "temperature-datawriter",
                Code = Code.FromAsset("src/Lambdas"),
                Runtime = Runtime.DOTNET_CORE_3_1,
                Handler = "Lambdas::Lambdas.TemperatureDataWriter::Handle"
            });
            temperatureDataWriter.AddEventSource(new SqsEventSource(temperatureSqsQueue));
        }
    }
}
