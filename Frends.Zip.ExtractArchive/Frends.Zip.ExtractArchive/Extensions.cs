using System.IO;
using System.Threading;

namespace Frends
{
    static class Extensions
    {
        internal static string GetNewFilename(string fullPath, string name, CancellationToken cancellationToken)
        {
            var index = 0;
            string newPath;
            do
            {
                cancellationToken.ThrowIfCancellationRequested();
                var new_Filename = $"{Path.GetFileNameWithoutExtension(name)}({index}){Path.GetExtension(name)}";
                newPath = Path.Combine(Path.GetDirectoryName(fullPath), new_Filename);
                index++;
            } while (File.Exists(newPath));
            return newPath;
        }
    }
}
