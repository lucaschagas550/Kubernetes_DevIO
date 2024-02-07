#!/bin/bash
# terminal 1
kubectl create -f deployment.yaml
kubectl get po -o wide

kubectl create -f service.yaml
kubectl port-forward service/service-demo-service 4200:80 #Consegue expor no browser o service na porta 4200 
                                                          #e fazer redirecionamento para o pod na porta 80
