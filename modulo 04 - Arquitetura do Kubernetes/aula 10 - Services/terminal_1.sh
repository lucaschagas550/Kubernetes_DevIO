#!/bin/bash
# terminal 1
kubectl create -f deployment.yaml
kubectl get po -o wide

kubectl create -f service.yaml
kubectl port-forward service/service-demo-service 4200:80
