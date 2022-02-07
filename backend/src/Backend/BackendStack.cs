using System.Collections.Generic;
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

            var devicesDataWriter = new DockerImageFunction(this, "devices-datawriter", new DockerImageFunctionProps{
                FunctionName = "devices-datawriter",
                Code = DockerImageCode.FromImageAsset("src/Lambdas", new AssetImageCodeProps{
                    Cmd = new string[] { "Lambdas::Lambdas.DeviceDataWriter::Handle" }
                }),
                Description = "Writes information to the devices dynamo-db table",
            });
            devicesDataWriter.AddEventSource(new SqsEventSource(devicesSqsQueue));


            /*
             * Temperature Data Writer
             */
            var temperatureSqsQueue = new Queue(this, "temperature-sqs", new QueueProps{
                QueueName = "temperature-datawriter",
                Encryption  = QueueEncryption.KMS
            });

            var temperatureDataWriter = new DockerImageFunction(this, "temperature-datawriter", new DockerImageFunctionProps{
                FunctionName = "temperature-datawriter",
                Code = DockerImageCode.FromImageAsset("src/Lambdas", new AssetImageCodeProps{
                    Cmd = new string[] { "Lambdas::Lambdas.TemperatureDataWriter::Handle" }
                }),
                Description = "Writes information to the temperature dynamo-db table"
            });
            temperatureDataWriter.AddEventSource(new SqsEventSource(temperatureSqsQueue));
        }
    }
}
