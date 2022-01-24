using System;
using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Constructs;

namespace Checkers
{
    /// <summary>
    /// Runs all checks on a cloudformation stack
    /// </summary>
    public class All : Amazon.JSII.Runtime.Deputy.DeputyBase, IAspect
    {
        public void Visit(IConstruct node)
        {
            if (node is CfnBucket)
            {
                var bucket = (CfnBucket)node;
                if (bucket.VersioningConfiguration is null ||
                    !Tokenization.IsResolvable(bucket.VersioningConfiguration) &&
                    !bucket.VersioningConfiguration.ToString().Contains("Enabled"))
                {
                    Annotations.Of(node).AddError("Bucket versioning is not enabled");
                }
            }
        }
    }
}
