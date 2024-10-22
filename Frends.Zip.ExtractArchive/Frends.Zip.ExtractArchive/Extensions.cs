using System.IO;
using System.Threading;
namespace Frends.Zip.ExtractArchive;

/// <summary>
/// Extensions class.
/// </summary>
static class Extensions
{
    internal static string GetNewFilename(string path, CancellationToken cancellationToken)
    {
        var index = 1;
        string newPath;
        do
        {
            cancellationToken.ThrowIfCancellationRequested();
            var new_Filename = $"{Path.GetFileNameWithoutExtension(path)}({index}){Path.GetExtension(path)}";
            newPath = Path.Combine(Path.GetDirectoryName(path), new_Filename);
            index++;
        } while (File.Exists(newPath));
        return newPath;
    }
}