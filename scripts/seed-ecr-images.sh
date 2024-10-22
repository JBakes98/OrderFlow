#!/bin/zsh

# Build Orderflow Events Publisher Lambda Docker image
docker build -t "000000000000.dkr.ecr.eu-west-2.localhost.localstack.cloud:4566/orderflow-events-publisher" . -f src/OrderFlow.Events.Publisher/Dockerfile
docker push "000000000000.dkr.ecr.eu-west-2.localhost.localstack.cloud:4566/orderflow-events-publisher"


# Build Orderflow API Docker image 
docker build -t "000000000000.dkr.ecr.eu-west-2.localhost.localstack.cloud:4566/orderflow-api" . -f src/OrderFlow.Api/Dockerfile
docker push "000000000000.dkr.ecr.eu-west-2.localhost.localstack.cloud:4566/orderflow-api"
