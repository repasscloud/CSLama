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

# Debugging: Log inputs for visibility
echo "Input directory: $input_dir"
echo "Output file: $output_file"
echo "Prepended text: $prepended_text"

# Ensure the output file is empty or create it if it doesn't exist
> "$output_file"

# Check if there are any .txt files in the input directory
if compgen -G "$input_dir/*.txt" > /dev/null; then
  # Loop through all .txt files in the directory, excluding the output file
  for file in "$input_dir"/*.txt; do
    # Skip the output file if it exists in the input directory
    if [ "$file" == "$output_file" ]; then
      continue
    fi

    # Read each line from the file and append modified lines to the output file
    while IFS= read -r line; do
      echo -e "${prepended_text}\t${line}" >> "$output_file"
    done < "$file"
  done
  echo "Lines processed and saved to $output_file"
else
  echo "No .txt files found in $input_dir. Exiting."
  exit 1
fi
