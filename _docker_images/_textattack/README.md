# Textattack

This is the dockerfile for the Textattack image, which is used to replace words in the data files for the ML

## Usage

1. Build the docker image

    ```sh
    docker build --rm -t textattack .
    ```

1. Run the docker image to replace the text to an output file from an input file

    ```sh
    docker run --rm -v "$(pwd):/app" textattack:latest python /app/text_attack_augmenter.py /app/input_text.txt /app/output_text.txt --transformations 4
    ```

1. You then need to merge the source file contents and the output into a single file again and use the "remove_duplicates.sh" file and the "merge_all_files_with_tab_value.sh" files to do their work again
