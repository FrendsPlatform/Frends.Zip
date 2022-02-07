# Frends.Zip.ExtractArchive
FRENDS tasks for extracting files from ZIP archive.

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT) 
[![Build](https://github.com/FrendsPlatform/Frends.Zip/actions/workflows/ExtractArchive_build_and_test_on_main.yml/badge.svg)](https://github.com/FrendsPlatform/Frends.Zip/actions)
![MyGet](https://img.shields.io/myget/frends-tasks/v/Frends.Zip.ExtractArchive)
![Coverage](https://app-github-custom-badges.azurewebsites.net/Badge?key=FrendsPlatform/Frends.Zip/Frends.Zip.ExtractArchive|main)

- [Installing](#installing)
- [Task](#task)
  - [ExtractArchive](#extractarchive)
- [Building](#building)
- [License](#license)
- [Contributing](#contributing)
- [Changelog](#changelog)

# Installing
You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed
'Insert nuget feed here'.

# Task

## ExtractArchive
Extracts files from a ZIP-archive.

### Task Properties

#### Input

| Property             | Type     | Description                   | Example                   |
|----------------------|----------|-------------------------------|---------------------------|
| SourceFile           | `string` | Full path to the zip-archive. | c:\source_folder\file.zip |
| Password             | `string` | (Optional) Archive password.  | secret                    |
| DestinationDirectory | `string` | Destination directory.        | c:\destination_directory\ |

#### Options

| Property                   | Type                           | Description                                                               | Example |
|----------------------------|--------------------------------|---------------------------------------------------------------------------|---------|
| FileExistAction            | enum<Error, Overwrite, Rename> | **Error**Throw error. **Overwrite**Overwrite file. **Rename**Rename file. | Error   |
| CreateDestinationDirectory | bool                           | Create destination directory if it does not exist.                        | true    |


### Result
| Property       | Type           | Description                | Example                         |
|----------------|----------------|----------------------------|---------------------------------|
| ExtractedFiles | List`<string>` | A list of extracted files. | {"file1.txt", "file2.txt", ...} |

# Building

Clone a copy of the repo.

`git clone https://github.com/CommunityHiQ/Frends.Zip.git`

Go to task directory.

`cd Frends.Zip/Frends.Zip.ExtractArchive`

Build the project.

`dotnet build`

Run tests.

`dotnet test`

Create a nuget package.

`dotnet pack --configuration Release`

# License

This project is licensed under the MIT License - see the LICENSE file for details.

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Changelog

| Version              | Changes                                              |
| ---------------------| -----------------------------------------------------|
| 1.0.0                | Initial implementation of Frends.Zip.ExtractArchive. |
