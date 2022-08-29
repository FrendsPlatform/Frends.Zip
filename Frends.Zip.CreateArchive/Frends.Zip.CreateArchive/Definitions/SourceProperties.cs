using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Zip.CreateArchive.Definitions;

/// <summary>
/// Source properties.
/// </summary>
public class SourceProperties
{
    /// <summary>
    /// Source files input type.
    /// </summary>
    /// <example>PathAndFileMask</example>
    [DefaultValue(SourceFilesType.PathAndFileMask)]
    public SourceFilesType SourceType { get; set; }

    /// <summary>
    /// Source directory.
    /// </summary>
    /// <example>C:\example\folder\</example>
    [UIHint(nameof(SourceType), "", SourceFilesType.PathAndFileMask)]
    [DisplayFormat(DataFormatString = "Text")]
    public string Directory { get; set; }

    /// <summary>
    /// The search string to match against the names of files. 
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions. 
    /// The default pattern is "*", which returns all files.
    /// </summary>
    /// <example>*</example>
    [DefaultValue("*")]
    [DisplayFormat(DataFormatString = "Text")]
    [UIHint(nameof(SourceType), "", SourceFilesType.PathAndFileMask)]
    public string FileMask { get; set; }

    /// <summary>
    /// Indicates if subfolders and files should also be zipped.
    /// </summary>
    /// <example>false</example>
    [DefaultValue(false)]
    [UIHint(nameof(SourceType), "", SourceFilesType.PathAndFileMask)]
    public bool IncludeSubFolders { get; set; }


    /// <summary>
    /// Choose if source folder structure should be flatten when zipped.
    /// </summary>
    /// <example>false</example>
    [DefaultValue(false)]
    [UIHint(nameof(SourceType), "", SourceFilesType.PathAndFileMask)]
    public bool FlattenFolders { get; set; }

    /// <summary>
    /// List&lt;string&gt; of full file paths to include in zip.
    /// </summary>
    /// <example>new List&lt;string&gt; { Directory.GetFiles(_dir, "*.txt")[0] };</example>
    [DisplayFormat(DataFormatString = "Expression")]
    [UIHint(nameof(SourceType), "", SourceFilesType.FileList)]
    public List<string> FilePathsList { get; set; }

    /// <summary>
    /// If true, files added to the zip are removed from source directory.
    /// </summary>
    /// <example>false</example>
    [DefaultValue(false)]
    public bool RemoveZippedFiles { get; set; }
}