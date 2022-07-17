using matrix.console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix.console.Helpers
{
    internal class MatrixHelper
    {
        public static MatrixDto GetData(string[] input)
        {
            try
            {
                Console.WriteLine("=======================================");
                Console.WriteLine("                MATRIX                 ");
                Console.WriteLine("=======================================");
                Console.WriteLine();

                int count = input.Length;

                if (count == 0)
                    throw new Exception("Debe cargar la matriz");

                string[,] result = new string[count, count];

                for (int i = 0; i < count; i++)
                {
                    var rowSplit = input[i].Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
                    var rowSplitTrim = rowSplit.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray(); // valida que no haya espacios en vez de caracteres
                    var isSquareMatrix = rowSplitTrim.Length == count;

                    if (!isSquareMatrix)
                        throw new Exception("La matriz debe ser cuadrada");

                    for (int j = 0; j < rowSplit.Length; j++)
                    {
                        result[i, j] = rowSplit[j].Trim().ToUpper();
                        Console.Write(result[i, j]);
                    }
                    Console.WriteLine();
                }
                return new MatrixDto { Matrix = result, Count = count };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
