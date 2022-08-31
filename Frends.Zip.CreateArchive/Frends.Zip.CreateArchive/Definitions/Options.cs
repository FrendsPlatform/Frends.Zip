using System.ComponentModel;

namespace Frends.Zip.CreateArchive.Definitions;

/// <summary>
/// Options.
/// </summary>
public class Options
{
    /// <summary>
    /// Always: Always use ZIP64 extensions when writing zip archives, even when unnecessary.
    /// AsNecessary: Use ZIP64 extensions when writing zip archives, as necessary (when single entry or entries in total exceeds 0xFFFFFFFF in size, or when there are more than 65535 entries).
    /// Never: Do not use ZIP64 extensions when writing zip archives.
    /// </summary>
    /// <example>AsNecessary</example>
    [DefaultValue(UseZip64Option.AsNecessary)]
    public UseZip64Option UseZip64 { get; set; }

    /// <summary>
    /// Throw error if no source files are found. Otherwise returns object with FileCount: 0.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorIfNoFilesFound { get; set; }

    /// <summary>
    /// Choose action if destination zip file already exists.
    /// Error: throws error.
    /// Overwrite: Overwrites existing zip file with new one.
    /// Rename: Renames new zip file (example.zip --&gt; example_(1).zip).
    /// Append: Adds new files to zip, if file already exists in zip, it is renamed.
    /// </summary>
    /// <example>Error</example>
    [DefaultValue(FileExistAction.Error)]
    public FileExistAction DestinationFileExistsAction { get; set; }

    /// <summary>
    /// Create destination folder if it does not exist.
    /// </summary>
    /// <example>false</example>
    [DefaultValue(false)]
    public bool CreateDestinationFolder { get; set; }
}