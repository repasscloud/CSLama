#!/bin/bash

# Check if three arguments are provided
if [ "$#" -ne 3 ]; then
  echo "Usage: $0 <input_directory> <output_file> <prepended_text>"
  exit 1
fi

# Positional parameters for input directory, output file, and prepended text
input_dir="$1"
output_file="$2"
prepended_text="$3"

# Ensure the output file is empty or create it if it doesn't exist
> "$output_file"

# Loop through all .txt files in the directory
for file in "$input_dir"/*.txt; do
  # Check if any .txt files exist
  if [ -e "$file" ]; then
    # Read each line from the file and append modified lines to the output file
    while IFS= read -r line; do
      echo -e "${prepended_text}\t${line}" >> "$output_file"
    done < "$file"
  fi
done

echo "Lines processed and saved to $output_file"
