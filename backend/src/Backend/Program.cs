using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            // Check for stack parameters
            var awsAccountId = System.Environment.GetEnvironmentVariable("AWS_ACCOUNT_ID") ?? "116827804402";
            var awsRegion = System.Environment.GetEnvironmentVariable("AWS_REGION") ?? "ap-southeast-2";
            var awsStackName = System.Environment.GetEnvironmentVariable("AWS_STACK_NAME") ?? "temperature-light-backend";

            var app = new App();
            var stack = new BackendStack(app, awsStackName, new StackProps
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = awsAccountId,
                    Region = awsRegion,
                },
                Description = "Temperature Lamp: The backend services that connect IoT devices, process and store data"
            });
            Tags.Of(stack).Add("project", "temperature-light");

            app.Synth();
        }
    }
}
