using matrix.console.Helpers;
using System;

namespace matrix.console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Ingrese el path del archivo txt y presione Enter (sino deje en blanco y se utilizará el predeterminado) :");
                string path = Console.ReadLine();

                string[] text = FileHelper.Read(path);
                string[,] matrix = MatrixHelper.GetData(text);
                MatrixHelper.GetLongString(matrix);

                Console.WriteLine("Presione enter para finalizar.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
