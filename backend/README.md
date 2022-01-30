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

## Database
There are two database tables. These are NoSQL DynamoDB databases. Timestamps are represented in [ISO format](https://en.wikipedia.org/wiki/ISO_8601).

### temperature-light-devices  
*This table stores current state of the temperature devices*

| Column                             | Type          | Key / Index |
| ---------------------------------- | ------------- | ----------- |
| device_id                          | string        | Primary     |
| latest_reading_iso_timestamp       | string        |             |
| latest_reading_temperature_celcius | number        | -           | 


### temperature-history
*This table stores the historical temperature data*

| Column              | Type          | Key / Index |
| ------------------- | ------------- | ----------- |
| device_id           | string        | Primary     |
| iso_timestamp       | string        | Secondary   |
| temperature_celcius | number        | -           | 