using System;
using System.IO;
using Ionic.Zip;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Frends.Zip.CreateArchive.Definitions;

namespace Frends.Zip.CreateArchive;

/// <summary>
/// Frends ZIP task.
/// </summary>
public class Zip
{
    /// <summary>
    /// Create zip file from selected files and/or folders. Created zip file content can be flatten and file can be protected with password.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Zip.CreateArchive)
    /// </summary>
    /// <returns>Object {string FilePath, int FileCount, List&lt;string&gt; ArchivedFiles}</returns>
    public static Result CreateArchive([PropertyTab] SourceProperties source, [PropertyTab] DestinationProperties destinationZip, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        // Validate that source and destination folders exist.
        if (!Directory.Exists(source.Directory) && source.SourceType == SourceFilesType.PathAndFileMask)
            throw new DirectoryNotFoundException($"Source directory {source.Directory} does not exist.");
        if (!Directory.Exists(destinationZip.Directory) && !options.CreateDestinationFolder)
            throw new DirectoryNotFoundException($"Destination directory {destinationZip.Directory} does not exist.");

        var sourceFiles = new List<string>();

        // Populate source files list according to input type.
        switch (source.SourceType)
        {
            case SourceFilesType.PathAndFileMask:
                sourceFiles = Directory.EnumerateFiles(source.Directory, source.FileMask, source.IncludeSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
                break;
            case SourceFilesType.FileList:
                sourceFiles = source.FilePathsList;
                break;
        }

        // If no files were found, throw error or return empty Output object.
        if (sourceFiles.Count == 0)
        {
            if (options.ThrowErrorIfNoFilesFound)
                throw new FileNotFoundException($"No files found in {source.Directory} with file mask '{source.FileMask}'");
            else
                return new Result("", 0, new List<string>());
        }

        // Check if destination directory exist and if it should be created.
        if (options.CreateDestinationFolder && !Directory.Exists(destinationZip.Directory))
            Directory.CreateDirectory(destinationZip.Directory);

        var destinationZipFileName = Path.Combine(destinationZip.Directory, destinationZip.FileName);

        // Check if destination zip exists.
        var destinationZipExists = File.Exists(destinationZipFileName);
        if (destinationZipExists)
            switch (options.DestinationFileExistsAction)
            {
                case FileExistAction.Error:
                    throw new Exception($"Destination file {destinationZipFileName} already exists.");

                case FileExistAction.Rename:
                    destinationZipFileName = GetRenamedZipFileName(destinationZipFileName);
                    break;
            }


        // Either create a new zip file or open existing one if Append was selected.
        using (var zipFile = (destinationZipExists && options.DestinationFileExistsAction == FileExistAction.Append) ? ZipFile.Read(destinationZipFileName) : new ZipFile())
        {
            // Set 'UseZip64WhenSaving' - needed for large zip files.
            zipFile.UseZip64WhenSaving = options.UseZip64.ConvertEnum<Zip64Option>();

            // If password is given, add it to archive.
            if (!string.IsNullOrWhiteSpace(destinationZip.Password)) zipFile.Password = destinationZip.Password;

            foreach (var fullPath in sourceFiles)
            {
                // Check if cancellation is requested.
                cancellationToken.ThrowIfCancellationRequested();

                // FlattenFolders = true: add all files to zip root, otherwise adda folders to zip. 
                // Only available when source type is path and filemask
                var pathInArchive = (source.FlattenFolders || source.SourceType == SourceFilesType.FileList) ? "" : fullPath.GetRelativePath(source.Directory);

                AddFileToZip(zipFile, fullPath, pathInArchive, destinationZip.RenameDuplicateFiles);
            }

            // Save zip (overwites existing file).
            zipFile.Save(destinationZipFileName);

            // Remove source files?
            foreach (var fullPath in sourceFiles) if (source.RemoveZippedFiles) File.Delete(fullPath);

            return new Result(destinationZipFileName, zipFile.Count, zipFile.EntryFileNames.ToList());
        }
    }

    private static void AddFileToZip(ZipFile zipFile, string fullPath, string pathInArchive, bool renameDublicateFile)
    {
        // Check is file with same name alredy added.
        if (zipFile.ContainsEntry(Path.GetFileName(fullPath)))
        {
            if (renameDublicateFile) RenameAndAddFile(zipFile, fullPath);
            else throw new Exception($"File {fullPath} already exists in zip!");
        }
        else zipFile.AddFile(fullPath, pathInArchive);
    }

    private static void RenameAndAddFile(ZipFile zipFile, string filePath)
    {
        var renamedFileName = GetRenamedFileName(zipFile.EntryFileNames, Path.GetFileName(filePath));
        zipFile.AddEntry(renamedFileName, File.ReadAllBytes(filePath));
    }

    private static string GetRenamedFileName(ICollection<string> existingFileNames, string fileName)
    {
        var index = 1;
        var renamedFile = fileName.RenameFile(index);
        while (existingFileNames.Contains(renamedFile))
        {
            index++;
            renamedFile = fileName.RenameFile(index);
        }

        return renamedFile;
    }

    private static string GetRenamedZipFileName(string fullPath)
    {
        var index = 1;
        var renamedFile = Path.GetFileName(fullPath).RenameFile(index);
        var path = Path.GetDirectoryName(fullPath);
        var renamedFileFullPath = Path.Combine(path, renamedFile);
        while (File.Exists(renamedFileFullPath))
        {
            index++;
            renamedFile = Path.GetFileName(fullPath).RenameFile(index);
            renamedFileFullPath = Path.Combine(path, renamedFile);
        }

        return renamedFileFullPath;
    }
}