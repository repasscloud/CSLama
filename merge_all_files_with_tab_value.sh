#!/bin/bash

# Check if two arguments are provided
if [ "$#" -ne 2 ]; then
  echo "Usage: $0 <input_directory> <output_file>"
  exit 1
fi

# Positional parameters for input directory and output file
input_dir="$1"
output_file="$2"

# Ensure the output file is empty or create it if it doesn't exist
> "$output_file"

# Loop through all .txt files in the directory
for file in "$input_dir"/*.txt; do
  # Check if any .txt files exist
  if [ -e "$file" ]; then
    # Read each line from the file and append modified lines to the output file
    while IFS= read -r line; do
      echo -e "${line}\tSome text here" >> "$output_file"
    done < "$file"
  fi
done

echo "Lines processed and saved to $output_file"
