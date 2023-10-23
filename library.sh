#!/bin/bash
DIRECTORY="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

if ! [[ -x "$(command -v c2cs)" ]]; then
  echo "Error: 'c2cs' is not installed. The C2CS tool is used to generate a C shared library for the purposes of P/Invoke with C#. Please visit https://github.com/bottlenoselabs/C2CS for instructions to install the C2CS tool." >&2
  exit 1
fi

c2cs library --config "$DIRECTORY/bindgen/config-build-c-library.json"
	
# Directory to search for libraries
search_dir="./lib"

# Check if the directory exists
if [ ! -d "$search_dir" ]; then
    echo "Directory not found: $search_dir"
    exit 1
fi

# Find all libraries with prefix "lib"
lib_files=$(find "$search_dir" -type f -name "lib*")

# Iterate through the list of found libraries and strip the "lib" prefix
for lib_file in $lib_files; do
    # Extract the filename without the path
    file_name=$(basename "$lib_file")
    
    # Strip the "lib" prefix
    new_name="${file_name#lib}"

    # Rename the file with the stripped prefix
    mv "$lib_file" "$(dirname "$lib_file")/$new_name"

    echo "Stripped and renamed: $file_name -> $new_name"
done	
	
	
if [[ -z "$1" ]]; then
    read
fi