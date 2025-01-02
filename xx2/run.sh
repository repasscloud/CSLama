#!/bin/bash

# Set repository information
REPO="repasscloud/CSLama"
BASE_URL="https://github.com/$REPO/releases/latest"

# Fetch the HTML of the latest release page
echo "Fetching latest release page..."
HTML=$(curl -Ls "$BASE_URL")
if [[ $? -ne 0 || -z "$HTML" ]]; then
    echo "Error: Unable to fetch the latest release page."
    exit 1
fi

# Save the HTML to a debug file
DEBUG_FILE="debug_latest_release.html"
echo "$HTML" > "$DEBUG_FILE"
echo "Saved the release page HTML to $DEBUG_FILE for inspection."

# Extract all ZIP file URLs
echo "Searching for ZIP file URLs in the latest release page..."
ZIP_URLS=$(echo "$HTML" | grep -oE "https://github.com/$REPO/releases/download/[^\"]+\.zip")
if [[ -z "$ZIP_URLS" ]]; then
    echo "Error: No ZIP file URLs found on the release page."
    echo "Inspect the saved HTML file: $DEBUG_FILE"
    exit 1
fi

# Log all found URLs
echo "Found the following ZIP URLs:"
echo "$ZIP_URLS"
