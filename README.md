# PDF Data Extractor CLI Application

This is a command-line interface (CLI) application built with C# .NET 8 for reading and extracting data from PDF files. The application utilizes iTextSharp library for PDF parsing and McMaster.Extensions.CommandLineUtils for creating a user-friendly CLI experience.

## Features

- **PDF Data Extraction:** Extracts specified data from PDF files.
- **Configurable Extraction:** Supports configuration for specifying the data to be extracted using regular expressions (REGEX).
- **Upcoming AI Integration:** Future plans include integrating AI for enhanced data extraction capabilities.

## Requirements

- .NET 8 runtime or SDK
- iTextSharp library
- McMaster.Extensions.CommandLineUtils

## Installation

1. Clone this repository to your local machine:

    ```bash
    git clone https://github.com/gisyLago/PDFExtractor.git
    ```

2. Navigate to the project directory:

    ```bash
    cd PDFExtractor
    ```

3. Build the project:

    ```bash
    dotnet build
    ```

## Usage

### Basic Usage

To extract data from a PDF file, use the following command:

```bash
dotnet PDFExtractor.dll --path <PDFFilePath>
```
```
Note: Make sure to replace <PDFFilePath> with appropriate file path.
```

### AI Integration (Upcoming)
AI integration for enhanced data extraction capabilities will be added soon. Stay tuned for updates!

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Acknowledgements
iTextSharp library: https://github.com/itext/itext7<br>
McMaster.Extensions.CommandLineUtils: https://github.com/natemcmaster/CommandLineUtils
