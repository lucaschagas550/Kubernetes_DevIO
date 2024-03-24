#!/bin/bash
docker build -f pod-lifecycle.failed/Dockerfile . -t desenvolvedorio/dominando-kubernetes:pod-lifecycle-failed
docker build -f pod-lifecycle.succeeded/Dockerfile . -t desenvolvedorio/dominando-kubernetes:pod-lifecycle-succeeded
docker build -f pod-lifecycle.running/Dockerfile . -t desenvolvedorio/dominando-kubernetes:pod-lifecycle-running

docker push desenvolvedorio/dominando-kubernetes:pod-lifecycle-failed
docker push desenvolvedorio/dominando-kubernetes:pod-lifecycle-succeeded
docker push desenvolvedorio/dominando-kubernetes:pod-lifecycle-running
