#!/bin/bash
# adicionar o alias k=kubectl no bashrc
echo "alias k=kubectl" >> ~/.bashrc
k run nginx --image=nginx --dry-run=client -o yaml > pod.yaml
k create -f pod.yaml
k get po
k describe po nginx
k get po nginx -o yaml
k get po nginx -o yaml > pod-criado.yaml