#!/bin/bash
kubectl run test-network --image=nicolaka/netshoot -i --tty --rm
curl 10.32.0.14
curl 10.32.0.8
curl 10.32.0.13
curl 10.101.206.69
curl http://service-demo-service.default