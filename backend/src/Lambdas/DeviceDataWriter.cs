using System;
using Amazon.Lambda.Core;

namespace Lambdas
{
    public class DeviceDataWriter
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Handle(object input, ILambdaContext context){
            Console.WriteLine("Hey World from Device Data Writer");
        }
    }
}
