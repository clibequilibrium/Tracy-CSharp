#!/bin/bash
DIRECTORY="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

function get_host_operating_system() {
    local UNAME_STRING="$(uname -a)"
    case "${UNAME_STRING}" in
        *Microsoft*)    local HOST_OS="windows";;
        *microsoft*)    local HOST_OS="windows";;
        Linux*)         local HOST_OS="linux";;
        Darwin*)        local HOST_OS="macos";;
        CYGWIN*)        local HOST_OS="linux";;
        MINGW*)         local HOST_OS="windows";;
        *Msys)          local HOST_OS="windows";;
        *)              local HOST_OS="UNKNOWN:${UNAME_STRING}"
    esac
    echo "$HOST_OS"
    return 0
}

OS="$(get_host_operating_system)"

if [[ "$OS" == "windows" ]]; then
    CASTFFI_CONFIG_FILE_PATH="$DIRECTORY/config-extract-windows.json"
elif [[ "$OS" == "macos" ]]; then
    CASTFFI_CONFIG_FILE_PATH="$DIRECTORY/config-extract-macos.json"
elif [[ "$OS" == "linux" ]]; then
    CASTFFI_CONFIG_FILE_PATH="$DIRECTORY/config-extract-linux.json"
else
    echo "Error: Unknown operating system '$OS'" >&2
    exit 1
fi

if ! [[ -x "$(command -v castffi)" ]]; then
  echo "Error: 'castffi' is not installed. Please visit https://github.com/bottlenoselabs/CAstFfi for instructions to install the CAstFfi tool." >&2
  exit 1
fi

castffi extract --config "$CASTFFI_CONFIG_FILE_PATH"
