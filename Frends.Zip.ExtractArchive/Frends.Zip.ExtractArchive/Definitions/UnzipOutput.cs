using System.Collections.Generic;
namespace Frends.Zip.ExtractArchive.Definitions;

/// <summary>
/// Output.
/// </summary>
public class UnzipOutput
{
    /// <summary>
    /// a List-object of extracted files.
    /// </summary>
    public List<string> ExtractedFiles { get; set; }

    internal UnzipOutput()
    {
        ExtractedFiles = new List<string>();
    }
}