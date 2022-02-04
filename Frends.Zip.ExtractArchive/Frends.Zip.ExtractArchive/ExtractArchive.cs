using System.IO;
using Ionic.Zip;
using System.Threading;
using System.ComponentModel;

#pragma warning disable 1591

namespace Frends.Zip.ExtractArchive
{
    public class Zip
    {
        /// <summary>
        /// A Frends task for extracting zip archives.
        /// See https://github.com/FrendsPlatform/Frends.Zip/Frends.Zip.ExtractArchive
        /// </summary>
        /// <param name="input">Input properties</param>
        /// <param name="options">Options</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Output-object with a List of extracted files</returns>
        public static UnzipOutput ExtractArchive(
            [PropertyTab] UnzipInputProperties input,
            [PropertyTab] UnzipOptions options,
            CancellationToken cancellationToken)
        {

            if (!File.Exists(input.SourceFile)) throw new FileNotFoundException($"Source file {input.SourceFile} does not exist.");

            if (!Directory.Exists(input.DestinationDirectory) && !options.CreateDestinationDirectory) throw new DirectoryNotFoundException($"Destination directory {input.DestinationDirectory} does not exist.");

            if (options.CreateDestinationDirectory) Directory.CreateDirectory(input.DestinationDirectory);

            var output = new UnzipOutput();

            using (var zip = ZipFile.Read(input.SourceFile))
            {
                string path = null;
                zip.ExtractProgress += (sender, e) => Zip_ExtractProgress(e, output, path);

                // If password is set.
                if (!string.IsNullOrWhiteSpace(input.Password)) zip.Password = input.Password;

                switch (options.DestinationFileExistsAction)
                {
                    case UnzipFileExistAction.Error:
                    case UnzipFileExistAction.Overwrite:
                        zip.ExtractExistingFile = (options.DestinationFileExistsAction == UnzipFileExistAction.Overwrite) ? ExtractExistingFileAction.OverwriteSilently : ExtractExistingFileAction.Throw;
                        zip.ExtractAll(input.DestinationDirectory);
                        break;
                    case UnzipFileExistAction.Rename:
                        foreach (var z in zip)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            if (File.Exists(Path.Combine(input.DestinationDirectory, z.FileName)))
                            {
                                // Find a filename that does not exist. 
                                var FullPath = Extensions.GetNewFilename(Path.Combine(Path.GetDirectoryName(input.DestinationDirectory), z.FileName), z.FileName, cancellationToken);
                                path = FullPath;

                                using (var fs = new FileStream(FullPath, FileMode.Create, FileAccess.Write)) z.Extract(fs);
                            }
                            else z.Extract(input.DestinationDirectory);
                        }
                        break;
                }
            }
            return output;
        }

        private static void Zip_ExtractProgress(ExtractProgressEventArgs e, UnzipOutput output, string fullPath)
        {
            if (e.EventType == ZipProgressEventType.Extracting_AfterExtractEntry && !e.CurrentEntry.IsDirectory)
            {
                // Path.GetFullPath changes directory separator to "\".
                if (e.ExtractLocation == null) output.ExtractedFiles.Add(Path.GetFullPath(fullPath));
                else output.ExtractedFiles.Add(Path.GetFullPath(Path.Combine(e.ExtractLocation, e.CurrentEntry.FileName)));
            }
        }
    }
}

