#!/bin/bash
k get po
k port-forward pod/nginx -n default 4200:80
k proxy
k get po -o wide
k run test-network --image=nicolaka/netshoot -i --tty
> curl http://172.17.0.3
k get po