using System;
using Amazon.Lambda.Core;

namespace Lambdas
{
    public class Default
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Handle(object input, ILambdaContext context){
            Console.WriteLine("Hello");
        }
    }
}
