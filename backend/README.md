# Backend

This is the backend infrastructure that connects up the IoT devices, processes and stores the data.
It is and AWS CDK project.

## Overview

// TODO diagram

    .
    ├── src                     # The source code
    │   ├── Backend             # Actual Infrastructure Code
    │   ├── Checkers            # CDK Aspects Checkers for best practices
    │   └── Backend.sln         
    └── README.md

## Getting started

1. Install the dotnet cli
2. Install the AWS CDK cli
3. `cdk bootstrap`
4. `dotnet restore`
5. `dotnet build src`

## Useful commands

* `dotnet build src` compile this app
* `cdk deploy`       deploy this stack to your default AWS account/region
* `cdk diff`         compare deployed stack with current state
* `cdk synth`        emits the synthesized CloudFormation template