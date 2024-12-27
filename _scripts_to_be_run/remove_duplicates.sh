#!/bin/bash

# Check if input file is provided
if [ "$#" -ne 1 ]; then
  echo "Usage: $0 <input_file>"
  exit 1
fi

# Positional parameter for input file
input_file="$1"

# Check if the file exists
if [ ! -f "$input_file" ]; then
  echo "Error: File '$input_file' not found!"
  exit 1
fi

# Remove duplicate lines and save to a temporary file
tmp_file=$(mktemp)
sort "$input_file" | uniq > "$tmp_file"

# Replace the original file with the cleaned file
mv "$tmp_file" "$input_file"

echo "Duplicate lines removed from '$input_file'."
