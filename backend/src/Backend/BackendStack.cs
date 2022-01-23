using Amazon.CDK;
using Amazon.CDK.AWS.SSM;
using Constructs;

namespace Backend
{
    public class BackendStack : Stack
    {
        internal BackendStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var dummyParameter = new StringParameter(this, "dummyParameter", new StringParameterProps{
                ParameterName = "/temperature-light/dummy",
                StringValue = "This should be removed once we have things in the stack"
            });
        }
    }
}
