using System;
using System.Linq;

namespace task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            byte rowsCount, columnsCount;
            int[,] matrix;
            InputMatrixSizes(out rowsCount, out columnsCount);
            matrix = InitMatrix(rowsCount, columnsCount);
            ShowMatrix(matrix);
            ShowResult(matrix);
        }

        static void InputMatrixSizes(out byte rowsCount, out byte columnsCount)
        {
            bool isOk = false;
            rowsCount = 10;
            columnsCount = 10;
            int tempCount1=0, tempCount2=0;
            while (!isOk)
            {
                try
                {
                    Console.Clear();
                    Console.Write("Введите число строк матрицы ({0}-{1}):", byte.MinValue+1, byte.MaxValue);
                    tempCount1 = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Введите число столбцов матрицы ({0}-{1}):", byte.MinValue+1, byte.MaxValue);
                    tempCount2 = Convert.ToInt32(Console.ReadLine());
                    isOk = true;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Неверный формат числа!!!");
                    isOk = false;
                }
                if (tempCount1 > 0 && tempCount1 <= byte.MaxValue && tempCount2 > 0 && tempCount2 <= byte.MaxValue)
                {
                    rowsCount = Convert.ToByte(tempCount1);
                    columnsCount = Convert.ToByte(tempCount2);
                }
                else
                    Console.WriteLine("Неверные размерности. Будут использованы значения по умолчанию.");
            }
        }

        static int[,] InitMatrix(byte rowsCount, byte columnsCount)
        {
            int[,] matrix = new int[rowsCount, columnsCount];
            Random generator = new Random();
            const int lowerBorder = -999, higherBorder=999;
            for(int i=0; i<rowsCount;i++)
                for(int j=0;j<columnsCount; j++)
                    matrix[i,j] = generator.Next(lowerBorder, higherBorder);
            return matrix;
        }

        static void ShowMatrix(int[,] matrix)
        {
            int min = int.MaxValue, max = int.MinValue;
            string format;
            int spacesCount = 2;
            foreach (int value in matrix)
            {
                if (value > max)
                    max = value;
                if (value < min)
                    min = value;
            }
            spacesCount += max.ToString().Length > min.ToString().Length ? max.ToString().Length : min.ToString().Length;
            format = "{0," + spacesCount.ToString() + ":d}";
            Console.WriteLine("Матрица:");
            Console.WriteLine("--------------------------------------------------------------------------");
            for (int i=0;i<matrix.GetLength(0);i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write(format, matrix[i,j]);
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------------------------------");
        }

        static int GetMaximalOfMinimalValuesInColumns(int[,] matrix)
        {
            int[] result = new int[matrix.GetLength(1)];
            for(int j=0; j<matrix.GetLength(1);j++)
            {
                int min = int.MaxValue;
                for (int i = 0; i < matrix.GetLength(0); i++)
                    if (matrix[i, j] < min)
                        min = matrix[i, j];
                result[j] = min;
            }
            return result.Max();
        }
        
        static int[,] GetIndexes(int[,] matrix, int value)
        {
            int[,] temp = new int[matrix.Length, 2];
            int[,] result;
            int index = 0;
            /*for(int i=0;i<temp.GetLength(0);i++)
            {
                temp[i, 0] = -1;
                temp[i, 1] = -1;
            }*/
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] == value)
                    {
                        temp[index, 0] = i;
                        temp[index, 1] = j;
                        index++;
                    }
            result = new int[index, 2];
            for (int i = 0; i < index; i++)
            {
                result[i, 0] = temp[i, 0];
                result[i, 1] = temp[i, 1];
            }
            return result;
        }

        static void ShowResult(int[,] matrix)
        {
            int max = GetMaximalOfMinimalValuesInColumns(matrix);
            int[,] indexes = GetIndexes(matrix, max);
            Console.Write("Значение масимального элемента среди минимальных элементов столбцов:");
            Console.WriteLine(max);
            Console.WriteLine("Данное значение содержиться в ячейке(ах):");
            for (int i = 0; i < indexes.GetLength(0); i++)
                Console.WriteLine($"Номер строки: {indexes[i,0]+1}, номер столбца: {indexes[i, 1] + 1}");
        }
    }
}
