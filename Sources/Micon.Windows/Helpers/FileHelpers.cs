﻿namespace Micon.Windows.Helpers
{
    public static class FileHelpers
    {
        public static void CreateFileIfNotExists(string path)
        {
            var dir = System.IO.Directory.GetParent(path);
            if (!dir.Exists)
            {
                dir.Create();
            }

            var file = new System.IO.FileInfo(path);
            if (!file.Exists)
            {
                file.Create().Dispose();
            }
        }
    }
}
