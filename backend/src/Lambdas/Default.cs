using System;
using Amazon.Lambda.Core;

namespace Lambdas
{
    public class Default
    {
        public void Handle(object input, ILambdaContext context){
            Console.WriteLine("Hello");
        }
    }
}
