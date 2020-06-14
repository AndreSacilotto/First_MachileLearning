using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_RL
{

    public static class Matematica
    {
        public static Random rng = new Random();

        public static double Q_Table(double q_table, double[] row_q_table, double learning_rate, double reward, double discount_rate)
        {
            return q_table * (1 - learning_rate) + learning_rate * (reward + discount_rate * row_q_table.Max());
        }

        public static double Exploration(double min_rate, double max_rate, double decay_rate, int episode)
        {
            return min_rate + (max_rate - min_rate) * E_pow(-(decay_rate) * episode);
        }

        public static double E_pow(double exp) => Math.Pow(Math.E ,exp);

        //public static float Random_Uniform(float min, float max)
        //{
        //    if (min == max)
        //        return min;

        //    return min + (max - min) * new Random().NextDouble(min, max);
        //}

        /// <summary>
        /// Entrega a posição do endereço de maior valor
        /// </summary>
        /// <param name="x">matriz 1D</param>
        public static int ArgMax<T>(T[] array)
        {
            return Array.IndexOf(array, array.Max());
        }

        // -------------------- Array Things --------------------

        public static T[] Array2D_flatter<T>(T[,] array)
        {
            var matrix = new T[array.Length];

            for (int i = 0; i < array.GetLength(0); i++)            
                for (int e = 0; e < array.GetLength(1); e++)
                    matrix[i] = array[i,e];

            return matrix;
        }

        public static T[] Array2D_GetRow<T>(T[,] array, int row)
        {
            if (row < 0 || row >= array.GetLength(0))
                return null;

            var matrix = new T[array.GetLength(1)];

            for (int i = 0; i < matrix.Length; i++)
                matrix[i] = array[row, i];

            return matrix;
        }

        public static T[][] ArrayJagged_Ini<T>(int a, int b)
        {
            T[][] matrix = new T[a][];
            for (int i = 0; i < matrix.Length; i++)
                matrix[i] = new T[b];

            return matrix;
        }

        public static int Array2D_GetFlatIndex<T>(T[,] array, int y, int x)
        {
            int arr_x_limit = array.GetLength(1);
            int r = x;

            for (int i = 0; i < y; i++)
                r += arr_x_limit;

            return r;
        }

    }
}
