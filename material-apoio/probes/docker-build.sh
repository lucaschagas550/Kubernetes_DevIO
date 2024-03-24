#!/bin/bash
docker build -f probes/Dockerfile . -t desenvolvedorio/dominando-kubernetes:probes

docker push desenvolvedorio/dominando-kubernetes:probes