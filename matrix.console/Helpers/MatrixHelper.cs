using System;
using System.Linq;

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

                //input = input.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
                int length = input.Length;

                if (length == 0)
                    throw new Exception("Error en el formato del archivo de texto: La matriz debe ser cuadrada");

                string[,] result = new string[length, length];

                for (int i = 0; i < length; i++)
                {
                    string[] rowSplit = Validate(i, input, length);

                    for (int j = 0; j < rowSplit.Length; j++)
                    {
                        result[i, j] = rowSplit[j].Trim();
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
        private static string[] Validate(int i, string[] input, int length)
        {
            string[] rowSplit = input[i].Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
            if (rowSplit.Length > length)
                throw new Exception("Error en el formato del archivo de texto: La matriz debe ser cuadrada.");

            //valido que no haya espacios vacios sin caracteres entre las comas (',')
            string[] rowSplitTrim = rowSplit.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
            //valido que esten ingresando un solo valor por fila/columna
            string[] rowSplitValidateOneChar = rowSplit.Where(x => x.Trim().Length == 1).ToArray();

            bool isSquareMatrix = rowSplitTrim.Length == length;
            bool hasOneCharPerRowCol = rowSplitValidateOneChar.Length == length;

            if (!isSquareMatrix)
                throw new Exception("Error en el formato del archivo de texto: La matriz debe ser cuadrada.");

            if (!hasOneCharPerRowCol)
                throw new Exception("Error en el formato del archivo de texto: La matriz debe contener solo un caracter por cada posicion de la matriz.");

            return rowSplit;
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

                    result = LongString(concatHorizontal, result);
                    result = LongString(concatVertical, result);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string LongString(string value, string result)
        {
            string stringLongHorizontal = GetString(value);
            if (stringLongHorizontal.Length > result.Length)
                result = stringLongHorizontal;

            return result;
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

                    result = LongString(concat, result);
                    concat = "";

                    // arranco del extremo inferior izquierdo (analizo ultima fila y primera columna) para obtener las diagonales secundarias 
                    // tomo la ultima row mientras i sea positivo, cuando pasa a negativo empiezo a subir por la columna 0
                    for (int row = (i >= 0 ? matrixLength - 1 : i + (matrixLength - 1)), col = (i >= 0 ? (matrixLength - 1) - i : 0); (row < matrixLength && row >= 0) && col < matrixLength; row--, col++)
                        concat = concat + matrix[row, col];

                    result = LongString(concat, result);
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
