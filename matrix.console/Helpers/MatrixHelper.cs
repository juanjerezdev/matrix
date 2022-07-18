using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace matrix.console.Helpers
{
    public class MatrixHelper
    {
        public static string[,] GetData(string[] input)
        {
            try
            {
                Console.WriteLine("=======================================");
                Console.WriteLine("                MATRIX                 ");
                Console.WriteLine("=======================================");
                Console.WriteLine();

                int length = input.Length;

                if (length == 0)
                    throw new Exception("Debe cargar la matriz");

                string[,] result = new string[length, length];

                for (int i = 0; i < length; i++)
                {
                    var rowSplit = input[i].Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
                    var rowSplitTrim = rowSplit.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray(); // valida que no haya espacios en vez de caracteres
                    var isSquareMatrix = rowSplitTrim.Length == length;

                    if (!isSquareMatrix)
                        throw new Exception("La matriz debe ser cuadrada");

                    for (int j = 0; j < rowSplit.Length; j++)
                    {
                        result[i, j] = rowSplit[j].Trim().ToUpper();
                        Console.Write(result[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public static void GetLongString(string[,] matrix)
        {
            try
            {
                int matrixLength = matrix.GetLength(0);
                string resultString = "";

                resultString = HorizontalVertical(matrix, resultString, matrixLength);
                resultString = Diagonal(matrix, resultString, matrixLength);

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
        private static string HorizontalVertical(string[,] matrix, string result, int matrixLength)
        {
            try
            {
                for (int i = 0; i < matrixLength; i++)
                {
                    string concatHorizontal = "";
                    string concatVertical = "";

                    for (int j = 0; j < matrixLength; j++)
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
        private static string Diagonal(string[,] matrix, string result, int matrixLength)
        {
            try
            {
                string concat = "";

                for (int i = matrixLength - 1; i > -matrixLength; i--)
                {
                    // arranco del extremo superior derecho (analizo primera fila y primera columna)
                    // cuando i se pone negativo (cambia de row) le hago el abs para que encuentre el index en la matriz
                    // la columna es siempre la i hasta que pasa a los negativo y le dejo cero para que tome la primer columna

                    for (int row = (i < 0 ? Math.Abs(i) : 0), col = (i > 0 ? i : 0); row < matrixLength && col < matrixLength; row++, col++)
                        concat = concat + matrix[row, col];

                    string stringLong = GetString(concat);
                    if (stringLong.Length > result.Length)
                        result = stringLong;

                    concat = "";

                    // arranco del extremo inferior izquierdo (analizo ultima fila y primera columna) para obtener las diagonales secundarias 
                    // tomo la ultima row mientras i sea positivo, cuando pasa a negativo empiezo a subir por la columna 0
                    for (int row = (i >= 0 ? matrixLength - 1 : i + (matrixLength - 1)), col = (i >= 0 ? (matrixLength - 1) - i : 0); (row < matrixLength && row >= 0) && col < matrixLength; row--, col++)
                    {
                        concat = concat + matrix[row, col];
                    }
                    stringLong = GetString(concat);
                    if (stringLong.Length > result.Length)
                        result = stringLong;

                    concat = "";
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
