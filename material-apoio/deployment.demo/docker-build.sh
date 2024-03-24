#!/bin/bash
docker build -f deployment.demo/Dockerfile . -t desenvolvedorio/dominando-kubernetes:deployment-demo

docker push desenvolvedorio/dominando-kubernetes:deployment-demo