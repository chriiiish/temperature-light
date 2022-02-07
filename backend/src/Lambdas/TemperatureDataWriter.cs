using System;
using Amazon.Lambda.Core;

namespace Lambdas
{
    public class TemperatureDataWriter
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Handle(object input, ILambdaContext context){
            Console.WriteLine("Hey World from Temperature Data Writer");
        }
    }
}
