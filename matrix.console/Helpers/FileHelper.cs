using System;
using System.IO;
using System.Text;

namespace matrix.console.Helpers
{
    public class FileHelper
    {
        public static string[] Read(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    path = @"./Matrix.txt";

                return File.ReadAllLines(path, Encoding.Default);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
