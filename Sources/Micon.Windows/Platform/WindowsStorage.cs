namespace Micon.Windows.Platform
{
    using System;
    using System.Threading.Tasks;
    using Portable.Platform;
    using System.IO;
    using Helpers;

    public class WindowsStorage : IStorage
    {
        public async Task<string> Load(string path)
        {
            if(File.Exists(path))
            {
                using (var sr = new StreamReader(path))
                {
                    return await sr.ReadToEndAsync();
                }
            }

            throw new InvalidOperationException("File doesn't exist");
        }

        public async Task Save(string path, string content)
        {
            FileHelpers.CreateFileIfNotExists(path);
            
            using (StreamWriter outputFile = new StreamWriter(path))
            {
               await outputFile.WriteAsync(content);
            }
        }
    }
}
