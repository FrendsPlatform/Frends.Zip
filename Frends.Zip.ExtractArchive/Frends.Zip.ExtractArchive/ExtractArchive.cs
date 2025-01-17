﻿using System;
using System.IO;
using Ionic.Zip;
using System.Threading;
using System.ComponentModel;
using Frends.Zip.ExtractArchive.Definitions;

namespace Frends.Zip.ExtractArchive;

/// <summary>
/// ZIP task.
/// </summary>
public class Zip
{
    /// <summary>
    /// A Frends task for extracting zip archives.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Zip.ExtractArchive)
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="options">Options parameters.</param>
    /// <param name="cancellationToken">Token generated by Frends to stop this task.</param>
    /// <returns>Object { List&lt;string&gt; ExtractedFiles }</returns>
    public static UnzipOutput ExtractArchive([PropertyTab] UnzipInputProperties input, [PropertyTab] UnzipOptions options, CancellationToken cancellationToken)
    {

        if (!File.Exists(input.SourceFile)) throw new FileNotFoundException($"Source file {input.SourceFile} does not exist.");
        if (!Directory.Exists(input.DestinationDirectory) && !options.CreateDestinationDirectory) throw new DirectoryNotFoundException($"Destination directory {input.DestinationDirectory} does not exist.");
        if (options.CreateDestinationDirectory) Directory.CreateDirectory(input.DestinationDirectory);

        var output = new UnzipOutput();

        using (var zip = ZipFile.Read(input.SourceFile))
        {
            string path = null;
            zip.ExtractProgress += (sender, e) => Zip_ExtractProgress(e, output, path);

            if (!string.IsNullOrWhiteSpace(input.Password)) zip.Password = input.Password;

            switch (options.DestinationFileExistsAction)
            {
                case UnzipFileExistAction.Error:
                case UnzipFileExistAction.Overwrite:
                    zip.ExtractExistingFile = (options.DestinationFileExistsAction == UnzipFileExistAction.Overwrite) ? ExtractExistingFileAction.OverwriteSilently : ExtractExistingFileAction.Throw;
                    zip.ExtractAll(input.DestinationDirectory);
                    break;
                case UnzipFileExistAction.Rename:
                    foreach (var z in zip)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        if (File.Exists(Path.Combine(input.DestinationDirectory, z.FileName)))
                        {
                            // Find a filename that does not exist. 
                            path = Extensions.GetNewFilename(Path.Combine(input.DestinationDirectory, z.FileName), cancellationToken);

                            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                            z.Extract(fs);
                        }
                        else
                        {
                            z.Extract(input.DestinationDirectory);
                        }
                    }
                    break;
            }
        }

        if (options.DeleteZipFileAfterExtract)
        {
            try
            {
                File.Delete(input.SourceFile);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Extraction was completed but an exception was thrown when trying to delete the source file: {ex.Message}");
            }
        }

        return output;
    }

    private static void Zip_ExtractProgress(ExtractProgressEventArgs e, UnzipOutput output, string fullPath)
    {
        if (e.EventType == ZipProgressEventType.Extracting_AfterExtractEntry && !e.CurrentEntry.IsDirectory)
        {
            // Path.GetFullPath changes directory separator to "\".
            if (e.ExtractLocation == null) output.ExtractedFiles.Add(Path.GetFullPath(fullPath));
            else output.ExtractedFiles.Add(Path.GetFullPath(Path.Combine(e.ExtractLocation, e.CurrentEntry.FileName)));
        }
    }
}