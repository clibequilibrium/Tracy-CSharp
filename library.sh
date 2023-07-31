#!/bin/bash
DIRECTORY="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

if ! [[ -x "$(command -v c2cs)" ]]; then
  echo "Error: 'c2cs' is not installed. The C2CS tool is used to generate a C shared library for the purposes of P/Invoke with C#. Please visit https://github.com/bottlenoselabs/C2CS for instructions to install the C2CS tool." >&2
  exit 1
fi

c2cs library --config "$DIRECTORY/bindgen/config-build-c-library.json"
	
if [[ -z "$1" ]]; then
    read
fi