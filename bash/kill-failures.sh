#!/bin/bash
set -o errexit
set -o nounset
set -o pipefail

# DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
NAMESPACE="${1:?"Missing namespace"}"
DEPLOYMENT="${2:?"Missing deployment"}"


FAILING_PODS=($(kubectl get pods -o wide -n "${NAMESPACE}" | rg "${DEPLOYMENT}" | rg -v "Running" | awk '{print $1}'))

set -o xtrace
for failing_pod in "${FAILING_PODS[@]}"; do
    # forking so that I don't have to wait
    kubectl delete pod -n "${NAMESPACE}" "${failing_pod}" &
done
