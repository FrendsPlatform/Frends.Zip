using System.Collections.Generic;

namespace Frends.Zip.CreateArchive.Definitions;

/// <summary>
/// Task result.
/// </summary>
public class Result
{
    /// <summary>
    /// Full path to zip created.
    /// </summary>
    /// <example>c:\temp\zip_sample.zip</example>
    public string Path { get; private set; }

    /// <summary>
    /// Number of files in created zip file.
    /// </summary>
    /// <example>1</example>
    public int FileCount { get; private set; }

    /// <summary>
    /// List of files zipped.
    /// </summary>
    /// <example>TestFile.txt, TestFile2.txt</example>
    public List<string> ArchivedFiles { get; private set; }

    internal Result(string path, int fileCount, List<string> archivedFiles)
    {
        Path = path;
        FileCount = fileCount;
        ArchivedFiles = archivedFiles;
    }
}