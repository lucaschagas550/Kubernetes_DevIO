#!/bin/bash
k run pod-lifecycle-running --image=desenvolvedorio/dominando-kubernetes:pod-lifecycle-running
k run pod-lifecycle-failed --image=desenvolvedorio/dominando-kubernetes:pod-lifecycle-failed --dry-run=client -o yaml > pod-lifecycle-failed.yaml
k create -f pod-lifecycle-failed.yaml
k run pod-lifecycle-succeeded --image=desenvolvedorio/dominando-kubernetes:pod-lifecycle-succeeded --dry-run=client -o yaml > pod-lifecycle-succeeded.yaml
k create -f pod-lifecycle-succeeded.yaml

k create -f pod-lifecycle-succeeded.yaml
k create -f pod-lifecycle-failed.yaml