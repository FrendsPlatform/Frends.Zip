using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Zip.CreateArchive.Definitions;

/// <summary>
/// Destination properties.
/// </summary>
public class DestinationProperties
{
    /// <summary>
    /// Destination directory.
    /// </summary> 
    /// <example>c:\temp</example>
    [DisplayFormat(DataFormatString = "Text")]
    public string Directory { get; set; }

    /// <summary>
    /// Filename of the zip to create.
    /// </summary>
    /// <example>sample.zip</example>
    [DisplayFormat(DataFormatString = "Text")]
    public string FileName { get; set; }

    /// <summary>
    /// Add password protection to zip.
    /// </summary>
    /// <example>foobar123</example>
    [PasswordPropertyText]
    public string Password { get; set; }

    /// <summary>
    /// True: If source files contains duplicate names, they are renamed (example.txt --&gt; example_(1).txt).
    /// False: Throws error if duplicate file names are found.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool RenameDuplicateFiles { get; set; }
}