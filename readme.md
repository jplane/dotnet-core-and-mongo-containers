### Deploying an ASP.NET Core + MongoDB app to local Docker and remote Azure Container Service (w/ Kubernetes)

------------

Prerequisites:

- Install Docker on a local machine

    https://docs.docker.com/engine/installation/

- Install Azure CLI 2.0

    https://docs.microsoft.com/en-us/cli/azure/install-azure-cli

- Install .NET Core SDK

    https://www.microsoft.com/net/download/core

- Create a Kubernetes cluster in Azure

    https://docs.microsoft.com/en-us/azure/container-service/container-service-kubernetes-walkthrough

- Create a VHD disk in Azure for Mongo persistence

    https://github.com/colemickens/azure-kubernetes-demo

--------------------------------

UPDATE: Here's a screencast of these demo steps in action... https://channel9.msdn.com/Blogs/MVP-Azure/Containers-Two-Ways

--------------------------------

1. build web api app from command line

    - dotnet restore
    - dotnet build
    - dotnet publish -o published

1. build docker image locally

    - docker build -t {YOUR-DOCKER-ID}/the-app .

1. run docker image + mongodb locally

    - docker run -d -p 27017:27017 --name mongodb bitnami/mongodb:latest
    - docker run -d -p 80:80 --name webapi --link mongodb {YOUR-DOCKER-ID}/the-app

1. push app container to Docker Hub

    - docker login
    - docker push {YOUR-DOCKER-ID}/the-app

1. use Azure CLI to connect to ACS cluster

    - az login
    - az account set --subscription "{YOUR-AZURE-SUBSCRIPTION-ID}"
    - az acs kubernetes get-credentials --resource-group="{YOUR-ACS-RG-NAME}" --name="{YOUR-KUBE-ACS-NAME}"

1. push mongo service

    - kubectl apply -f mongo.service.yaml

1. push mongo deployment

    - update mongo.deployment.yaml
    - kubectl apply -f mongo.deployment.yaml

1. push app service

    - kubectl apply -f app.service.yaml

1. push app deployment

    - update app.deployment.yaml
    - kubectl apply -f app.deployment.yaml

1. admin UI

    - kubectl proxy
    - localhost:8001/ui
