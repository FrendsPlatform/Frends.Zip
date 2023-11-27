namespace Frends.Zip.CreateArchive.Definitions;

/// <summary>
/// Enumeration of encoding options.
/// </summary>
public enum FileEncoding
{
    // Pragma for self-explanatory enum attributes.
#pragma warning disable 1591
    UTF8,
    ANSI,
    ASCII,
    WINDOWS1252,
    /// <summary>
    /// Other enables users to add other encoding options as string.
    /// </summary>
    Other
}

