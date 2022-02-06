using System;
using Amazon.Lambda.Core;

namespace Lambdas
{
    public class TemperatureDataWriter
    {
        public void Handle(object input, ILambdaContext context){
            Console.WriteLine("Hey World from Temperature Data Writer");
        }
    }
}
