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
        public static void GetLongString(string[,] matrix, int count)
        {
            try
            {
                string resultString = "";
                resultString = HorizontalVertical(matrix, resultString, count);
                resultString = Diagonal(matrix, resultString, count);

                Console.WriteLine();
                Console.WriteLine("=======================================");
                Console.WriteLine("La cadena mas larga es: {0}", resultString);
                Console.WriteLine("=======================================");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetString(string value)
        {
            try
            {
                string longString = "";
                string longStringMax = "";
                char currentChar = value[0];

                foreach (char c in value)
                {
                    if (currentChar == c)
                        longString = longString + c;
                    else
                    {
                        if (longString.Length > longStringMax.Length)
                            longStringMax = longString;

                        longString = c.ToString();
                    }
                    currentChar = c;
                }
                return longString.Length > longStringMax.Length ? longString : longStringMax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string HorizontalVertical(string[,] matrix, string result, int count)
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    string concatHorizontal = "";
                    string concatVertical = "";

                    for (int j = 0; j < count; j++)
                    {
                        concatHorizontal = concatHorizontal + matrix[i, j];
                        concatVertical = concatVertical + matrix[j, i];
                    }

                    string stringLongHorizontal = GetString(concatHorizontal);
                    if (stringLongHorizontal.Length > result.Length)
                        result = stringLongHorizontal;

                    string stringLongVertical = GetString(concatVertical);
                    if (stringLongVertical.Length > result.Length)
                        result = stringLongVertical;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string Diagonal(string[,] matrix, string result, int count)
        {
            try
            {
                string concat = "";
                string concatMainDiagonal = "";

                for (int i = 0; i < count; i++)
                {
                    for (int j = count - 1; j >= 0; j--)
                    {
                        // diagonal principal
                        if (i == j)
                            concatMainDiagonal = concatMainDiagonal + matrix[i, j];

                        // busco los extremos y despues sus diagonales
                        if (i == 0 && j > 0 || i > 0 && j == 0)
                        {
                            var col = j + 1;
                            if (col <= count)
                            {
                                var cantidadColumna = Math.Abs(i + j - count);
                                for (int d = 0; d < cantidadColumna; d++)
                                    concat = concat + matrix[d + i, j + d];

                                string stringLong = GetString(concat);
                                if (stringLong.Length > result.Length)
                                    result = stringLong;

                                concat = "";
                            }
                        }
                    }
                }
                string stringLongMainDiagonal = GetString(concatMainDiagonal);
                if (stringLongMainDiagonal.Length > result.Length)
                    result = stringLongMainDiagonal;

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
