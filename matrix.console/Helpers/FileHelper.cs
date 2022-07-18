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

                string extension = Path.GetExtension(path);
                if (extension != ".txt")
                    throw new Exception("Error al intentar leer el archivo: Su extensión debe ser un .txt");

                if (!File.Exists(path))
                    throw new Exception("Error al intentar leer el archivo: No existe el path: " + path);

                // se agregó encoding para que pueda leer caracteres como la Ñ
                return File.ReadAllLines(path, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
