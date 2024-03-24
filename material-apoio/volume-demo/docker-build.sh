#!/bin/bash
docker build -f volume-demo.create/Dockerfile . -t desenvolvedorio/dominando-kubernetes:volume-demo-create
docker build -f volume-demo.read/Dockerfile . -t desenvolvedorio/dominando-kubernetes:volume-demo-read

docker push desenvolvedorio/dominando-kubernetes:volume-demo-create
docker push desenvolvedorio/dominando-kubernetes:volume-demo-read
