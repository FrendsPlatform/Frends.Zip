using System.ComponentModel;
namespace Frends.Zip.ExtractArchive.Definitions;

/// <summary>
/// Options.
/// </summary>
public class UnzipOptions
{
    /// <summary>
    /// Action to be taken when destination file/files exist.
    /// </summary>
    /// <example>Error</example>
    [DefaultValue(UnzipFileExistAction.Error)]
    public UnzipFileExistAction DestinationFileExistsAction { get; set; }

    /// <summary>
    /// Create destination directory if it does not exist.
    /// </summary>
    /// <example>false</example>
    [DefaultValue(false)]
    [DisplayName(@"Create destination directory")]
    public bool CreateDestinationDirectory { get; set; }

    /// <summary>
    /// If set to true, the source ZIP file will be deleted after successful extraction.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    [DisplayName("Delete ZIP file after extraction")]
    public bool DeleteZipFileAfterExtract { get; set; }
}