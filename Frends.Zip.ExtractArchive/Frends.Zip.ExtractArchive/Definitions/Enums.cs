namespace Frends.Zip.ExtractArchive.Definitions;

/// <summary>
/// Unzip file exist actions.
/// </summary>
public enum UnzipFileExistAction 
{
#pragma warning disable CS1591 // self explanatory.
    Error,
    Overwrite, 
    Rename
#pragma warning restore CS1591 // self explanatory.
};