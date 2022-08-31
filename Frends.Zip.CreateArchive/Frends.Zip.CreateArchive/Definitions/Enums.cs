namespace Frends.Zip.CreateArchive.Definitions;

/// <summary>
/// File exist actions.
/// </summary>
public enum FileExistAction
{
#pragma warning disable CS1591 // self explanatory
    Error,
    Append,
    Overwrite,
    Rename
#pragma warning restore CS1591 // self explanatory
};

/// <summary>
/// Source file's type.
/// </summary>
public enum SourceFilesType
{
#pragma warning disable CS1591 // self explanatory
    PathAndFileMask,
    FileList
#pragma warning restore CS1591 // self explanatory
}

/// <summary>
/// Use ZIP64 options.
/// </summary>
public enum UseZip64Option
{
#pragma warning disable CS1591 // self explanatory
    Always,
    AsNecessary,
    Never
#pragma warning restore CS1591 // self explanatory
};