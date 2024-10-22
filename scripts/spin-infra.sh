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

