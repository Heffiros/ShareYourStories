#!/bin/bash

# Build the Docker image
docker build -t lovecraft-api .

# Stop and remove the running container (if any)
docker ps -q --filter "ancestor=lovecraft-api" | xargs -r docker stop
docker ps -aq --filter "ancestor=lovecraft-api" | xargs -r docker rm

# Run a new container
docker run -it -p 8080:8080 -p 8081:8081 lovecraft-api
