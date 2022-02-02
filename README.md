# Frends.Zip
FRENDS tasks for creating zip archive

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
  - [Create Archive](#createarchive)
  - [Extract Archive](#extractarchive)
- [License](#license)
- [Building](#building)
- [Contributing](#contributing)

# Installing
You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed
'Insert nuget feed here'

# Tasks

## CreateArchive
The CreateArchive-task is meant for creating zip file from selected files and/or folders. Created zip file content can be flatten and file can be protected with password.

### Task Properties

#### Source

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| Source type | enum<PathAndFileMask, FileList> | **PathAndFileMask:** Source files are fetched by given directory and filemask. **FileList:** Source files are given as a list of full paths to file |
| Directory | string | Source file(s) directory. | c:\source_folder\ |
| File mask | string | The search string to match against the names of files. Supports wildcards '?' and '*'. | * |
| File paths list | List<string> | A list of full paths to files to be added to zip |
| Remove zipped files | bool | **true:** Removes files from source path that are added to zip. **false:** Files added to zip are left at source path. |
| Include sub folders | bool | Indicates if sub folders in Path property should be included in search with file mask. | false |
| Flatten folders | bool | Choose if source folder structure should be flatten when zipped. | false |

#### Destination

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| Directory | string | Directory for zip file created. | c:\destination_folder\ |
| File name | string | Name of zip file created. | example.zip |
| Password | string | If set, zip archive will be password protected. | |
| Rename duplicate files | bool | If source files contains duplicate names, they are renamed (example.txt --&gt; example_(1).txt) | true |

#### Options

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| Use zip64 | enum<Always, AsNecessary, Never> | **Always:** Always use ZIP64 extensions when writing zip archives, even when unnecessary. **AsNecessary:** Use ZIP64 extensions when writing zip archives, as necessary (when single entry or entries in total exceeds 0xFFFFFFFF in size, or when there are more than 65535 entries) **Never:** Do not use ZIP64 extensions when writing zip archives. | AsNecessary |
| Throw error if no files found | bool | If no files were found in source path, throw exception. Otherwise returns object with FileCount = 0 | true |
| Destination file exists action | enum <Error, Append, Overwrite, Rename> | What to do if destination zip file already exists. | Rename (renames zip file: example.zip --&gt; example_(1).zip) |
| Create destination folder | bool | True: creates destination folder if it does not exist. False: throws error if destination folder is not found. | false |


### Result

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| Path | string | Full path to zip file created. | 'C:\my_zips\my_zipfile.zip' |
| FileCount | int | Number of files added to zip archive | 10 |
| ArchivedFiles | List&lt;string&gt; | File names with relative path in zip archive | {'file_1.txt', 'file_2.txt', 'sub_folder/file_3.txt'} |

## ExtractArchive
Extracts files from a zip-archive.

### Task Properties

#### Input

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| SourceFile | string | Full path to the zip-archive | c:\source_folder\file.zip |
| Password | string | (Optional) Archive password | secret |
| DestinationDirectory | string | Destination directory | c:\destination_directory\ |

#### Options

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| FileExistAction | enum(Error, Overwrite, Rename) | Throw error, Overwrite file or Rename file | Error |
| CreateDestinationDirectory | bool | Create destination directory if it does not exist | true|


### Result
| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| ExtractedFiles | List`<string>` | a List of extracted files | {"file1.txt", "file2.txt", ...}|

# License

This project is licensed under the MIT License - see the LICENSE file for details

# Building

Clone a copy of the repo.

`git clone https://github.com/CommunityHiQ/Frends.Zip.git`

Go to package directory.

`cd Frends.Zip/Frends.Zip.CreateArchive`
`cd Frends.Zip/Frends.Zip.ExtractArchive`

Build the project.

`dotnet build`

Run tests.

`dotnet test`

Create a nuget package.

`dotnet pack --configuration Release Frends.Zip.CreateArchive`
`dotnet pack --configuration Release Frends.Zip.ExtractArchive`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!
