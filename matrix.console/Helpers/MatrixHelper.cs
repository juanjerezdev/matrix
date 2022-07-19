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


                input = input.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
                int length = input.Length;

                if (length == 0)
                    throw new Exception("Error en el formato del archivo de texto: debe cargar una matriz");

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
            //tener en cuenta que los elementos de la matriz pueden ser cualquier caracter a excepcion de la coma y el espacio ya que se usan como separadores

            //REGEX
            //paginas utilizadas para hacer la regex
            //https://regex101.com/
            //https://regexr.com/

            // ^ -> obliga que el comienzo matchee con el siguiente patron, en este caso (?!,).
            //(?!,). -> el . es cualquier caracter pero con la excepcion de que empiece con la ','. (?!) -> hace que niegue el caracter coma
            // los parentesis agrupan
            // (, (?! )(?!,).)* -> este grupo dice que el caracter siguiente debe ser una coma, luego un espacio y el siguiente cualquier caracter (.) que sea distinto a un espacio y una coma. El * sirve para que le patron anterior, coincida 0 o mas veces.
            // \n? -> salto de linea opcional, ya que la ultima fila quizas no tenga salto de linea
            // $ -> se utiliza para matchee el final, y en conjunto con (^) se utiliza para que todo el string matchee completo
            string pattern = @"^(?!,).(, (?! )(?!,).)*\n?$";
            var regex = new Regex(pattern, RegexOptions.Singleline);
            bool isRegex = regex.IsMatch(input[i]);
            if (!isRegex)
                throw new Exception("Error en el formato del archivo de texto: verifique que cada fila de la matriz cumpla el siguiente patron, por ejemplo en una matriz 4x4 -> 'c, c, c, c' (elemento de la matriz separado por una coma y un espacio)");

            string[] rowSplit = input[i].Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
            bool isSquareMatrix = rowSplit.Length == length;

            if (!isSquareMatrix)
                throw new Exception("Error en el formato del archivo de texto: La matriz debe ser cuadrada.");

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
