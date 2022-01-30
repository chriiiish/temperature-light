using Amazon.CDK;
using Amazon.CDK.AWS.SSM;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.DynamoDB;
using Constructs;

namespace Backend
{
    public class BackendStack : Stack
    {
        internal BackendStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
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
        }
    }
}
