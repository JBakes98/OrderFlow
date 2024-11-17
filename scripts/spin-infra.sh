#!/bin/zsh
cd ../

# Start docker engine
open -a Docker
sleep 5

# Start Docker images
docker compose -f ./docker-compose.infra.yml -f ./docker-compose.logging.yml up -d --build

# Spin up Localstack Iac
cd ./iac
terraform init
tflocal apply -auto-approve

# Force restart the RDS postgres instance to apply the parameter group
awslocal rds modify-db-instance --db-instance-identifier orderflow-postgres-instance --db-parameter-group-name "orderflow-rds-pg" > /dev/null 2>&1

# Move to the API directory
cd ../src/Orderflow.Api/

# Run entity framework update command to create SQL tables
dotnet ef database update 

