#!/bin/bash
docker build -f conversao-moedas/Dockerfile . -t desenvolvedorio/dominando-kubernetes:conversao-moedas

docker push desenvolvedorio/dominando-kubernetes:conversao-moedas