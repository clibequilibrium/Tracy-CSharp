#!/bin/bash
DIRECTORY="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

if ! [[ -x "$(command -v c2cs)" ]]; then
  echo "Error: 'c2cs' is not installed. Please visit https://github.com/bottlenoselabs/C2CS for instructions to install the C2CS tool." >&2
  exit 1
fi

c2cs generate --config "$DIRECTORY/config-generate-cs.json"