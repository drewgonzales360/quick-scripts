#!/bin/bash
set -o errexit
set -o nounset
set -o pipefail

# DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
# # shellcheck disable=SC1091,SC1090
# source "${DIR}/vault-common.bash"

# UNIT="${1:?"Missing unit name"}"
# ENV="${2:?"Missing env"}"


NAMESPACE="${1}"
DEPLOYMENT="${2}"
FAILING_NODES=($(kubectl get pods -o wide -n "${NAMESPACE}" | rg "${DEPLOYMENT}" | rg "Running" | awk '{print $7}'))

for failing_node in "${FAILING_NODES[@]}"; do
    echo "

    ${failing_node}:"
    kubectl describe node "${failing_node}" | tail
done
