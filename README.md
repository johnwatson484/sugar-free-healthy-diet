[![Build Status](https://johnwatson484.visualstudio.com/John%20D%20Watson/_apis/build/status/Sugar%20Free%20Healthy%20Diet?branchName=master)](https://johnwatson484.visualstudio.com/John%20D%20Watson/_build/latest?definitionId=31&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=johnwatson484_sugar-free-healthy-diet&metric=alert_status)](https://sonarcloud.io/dashboard?id=johnwatson484_sugar-free-healthy-diet)

# Sugar Free Healthy Diet
Create and publish recipes

# Prerequisites
- Docker
- Docker Compose

# Running the application
The application is intended to be run and developed within a container.  A set of docker-compose files exist to support this.

## Run production application in container 
Note the application is dependent on an external PostgreSQL database.  A connection string in the environment variable `ConnectionStrings__SugarFreeHealthyDiet` is required.

The application will be accessible on port `5000`.

```
docker-compose -f docker-compose.yaml build
docker-compose -f docker-compose.yaml up
```

## Develop application in container
This will create a PostgreSQL database in a separate container exposed on port `5432`.  The application will be accessible on port `5000`.

```
docker-compose build
docker-compose up
```

## Debug application in container
Running the above development container configuration will include a remote debugger that can be connected to using the example VS Code debug configuration within `./vscode`.

The `${command:pickRemoteProcess}` will prompt for which process to connect to within the container.  

Local changes to code files will automatically trigger a rebuild and restart of the application within the container.

## Run tests
Unit tests are written in NUnit.

```
docker-compose -f docker-compose.test.yaml build
docker-compose -f docker-compose.test.yaml up
```

## Code coverage
Code coverage can be collected by changing the test container command to use [Coverlet MS Build integration](https://github.com/tonerdo/coverlet/blob/master/Documentation/MSBuildIntegration.md).

`dotnet test /p:CollectCoverage=true /p:CoverletOutput='./TestResults/`
