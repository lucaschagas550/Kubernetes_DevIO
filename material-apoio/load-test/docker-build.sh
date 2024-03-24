#!/bin/bash
docker build -f LoadTest/Dockerfile . -t desenvolvedorio/dominando-kubernetes:load-test

docker push desenvolvedorio/dominando-kubernetes:load-test
