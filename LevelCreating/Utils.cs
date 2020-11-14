using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelCreating
{
    static class Utils
    {
        public static void Populate<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }

        public static void Populate<T>(this T[,] arr, T value)
        {
            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for(int j = 0; j < arr.GetLength(1); ++j)
                {
                    arr[i,j] = value;
                }
            }
        }
    }
}
