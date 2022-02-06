using System;
using Amazon.Lambda.Core;

namespace Lambdas
{
    public class DeviceDataWriter
    {
        public void Handle(object input, ILambdaContext context){
            Console.WriteLine("Hey World from Device Data Writer");
        }
    }
}
