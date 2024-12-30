import argparse
from textattack.augmentation import WordNetAugmenter

def augment_file(input_file, output_file, transformations_per_example=3):
    # Define the augmenter
    augmenter = WordNetAugmenter(transformations_per_example=transformations_per_example)

    # Read lines from the input file
    with open(input_file, 'r') as infile:
        lines = infile.readlines()

    # Open the output file for writing
    with open(output_file, 'w') as outfile:
        for line in lines:
            line = line.strip()
            if not line:
                continue  # Skip empty lines

            # Generate augmentations
            augmented = augmenter.augment(line)

            # Write only augmented lines to the output file
            for aug in augmented:
                outfile.write(f"{aug}\n")

    print(f"Augmentation complete. Results saved to {output_file}")

if __name__ == "__main__":
    # Define argument parser
    parser = argparse.ArgumentParser(description="Text augmentation using TextAttack WordNetAugmenter.")
    parser.add_argument("input_file", help="Path to the input file containing text to augment.")
    parser.add_argument("output_file", help="Path to the output file to save augmented text.")
    parser.add_argument(
        "--transformations", 
        type=int, 
        default=3, 
        help="Number of augmentations per input line (default: 3)."
    )

    args = parser.parse_args()

    # Call the augmentation function
    augment_file(args.input_file, args.output_file, args.transformations)
