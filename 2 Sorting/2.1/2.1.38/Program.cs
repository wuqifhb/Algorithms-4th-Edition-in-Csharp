﻿using System;
using Sort;

namespace _2._1._38
{
    /*
     * 2.1.38
     * 
     * 不同类型的元素。
     * 编写一个测试用例，生成由多种数据类型元素组成的数组，元素的主键值随机，包括：
     * 每个元素的主键均为 String 类型（至少长 10 个字符），并含有一个 double 值。
     * 每个元素的主键均为 double 类型，并含有 10 个 String 值（每个都至少长 10 个字符）。
     * 每个元素的主键均为 int 类型，并含有一个 int[20] 值。
     * 评估并验证这些输入数据对本节讨论的算法的性能的影响。
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10000;

            double[] results = TestA(n);
            Console.WriteLine("string + double");
            Console.WriteLine("Insertion Sort:" + results[0]);
            Console.WriteLine("Selection Sort:" + results[1]);
            Console.WriteLine("Shell Sort:" + results[2]);

            results = TestB(n);
            Console.WriteLine("double + 10 string");
            Console.WriteLine("Insertion Sort:" + results[0]);
            Console.WriteLine("Selection Sort:" + results[1]);
            Console.WriteLine("Shell Sort:" + results[2]);

            results = TestC(n);
            Console.WriteLine("int + int[]");
            Console.WriteLine("Insertion Sort:" + results[0]);
            Console.WriteLine("Selection Sort:" + results[1]);
            Console.WriteLine("Shell Sort:" + results[2]);
        }

        /// <summary>
        /// 第一个测试，测试结果按照 Insertion, Selection, Shell 排序。
        /// </summary>
        /// <param name="n">测试的数组长度。</param>
        /// <returns>测试结果。</returns>
        static double[] TestA(int n)
        {
            InsertionSort insertionSort = new InsertionSort();
            SelectionSort selectionSort = new SelectionSort();
            ShellSort shellSort = new ShellSort();

            Random random = new Random();

            // 每个元素的主键均为 String 类型（至少长 10 个字符），并含有一个 double 值。
            Pair<string, double>[] array = new Pair<string, double>[n];
            Pair<string, double>[] arrayBak = new Pair<string, double>[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = new Pair<string, double>(RandomString(20, random), random.NextDouble());
            }
            array.CopyTo(arrayBak, 0);

            double[] results = new double[3];
            results[0] = SortCompare.Time(insertionSort, array);
            arrayBak.CopyTo(array, 0);
            results[1] = SortCompare.Time(selectionSort, array);
            arrayBak.CopyTo(array, 0);
            results[2] = SortCompare.Time(shellSort, array);
            return results;
        }

        /// <summary>
        /// 第二个测试，测试结果按照 Insertion, Selection, Shell 排序。
        /// </summary>
        /// <param name="n">测试的数组长度。</param>
        /// <returns>测试结果。</returns>
        static double[] TestB(int n)
        {
            InsertionSort insertionSort = new InsertionSort();
            SelectionSort selectionSort = new SelectionSort();
            ShellSort shellSort = new ShellSort();

            Random random = new Random();

            // 每个元素的主键均为 double 类型，并含有 10 个 String 值（每个都至少长 10 个字符）。
            Pair<double, string[]>[] array = new Pair<double, string[]>[n];
            Pair<double, string[]>[] arrayBak = new Pair<double, string[]>[n];
            for (int i = 0; i < n; i++)
            {
                string[] temp = new string[10];
                for (int j = 0; j < 10; j++)
                {
                    temp[j] = RandomString(12, random);
                }
                array[i] = new Pair<double, string[]>(random.NextDouble(), temp);
            }
            array.CopyTo(arrayBak, 0);

            double[] results = new double[3];
            results[0] = SortCompare.Time(insertionSort, array);
            arrayBak.CopyTo(array, 0);
            results[1] = SortCompare.Time(selectionSort, array);
            arrayBak.CopyTo(array, 0);
            results[2] = SortCompare.Time(shellSort, array);
            return results;
        }

        /// <summary>
        /// 第三个测试，测试结果按照 Insertion, Selection, Shell 排序。
        /// </summary>
        /// <param name="n">测试的数组长度。</param>
        /// <returns>测试结果。</returns>
        static double[] TestC(int n)
        {
            InsertionSort insertionSort = new InsertionSort();
            SelectionSort selectionSort = new SelectionSort();
            ShellSort shellSort = new ShellSort();

            Random random = new Random();

            // 每个元素的主键均为 int 类型，并含有一个 int[20] 值。
            Pair<int, int[]>[] array = new Pair<int, int[]>[n];
            Pair<int, int[]>[] arrayBak = new Pair<int, int[]>[n];
            for (int i = 0; i < n; i++)
            {
                int[] temp = new int[20];
                for (int j = 0; j < 20; j++)
                {
                    temp[j] = random.Next();
                }
                array[i] = new Pair<int, int[]>(random.Next(), temp);
            }
            array.CopyTo(arrayBak, 0);

            double[] results = new double[3];
            results[0] = SortCompare.Time(insertionSort, array);
            arrayBak.CopyTo(array, 0);
            results[1] = SortCompare.Time(selectionSort, array);
            arrayBak.CopyTo(array, 0);
            results[2] = SortCompare.Time(shellSort, array);
            return results;
        }


        /// <summary>
        /// 获取一个随机 <see cref="string"/>。
        /// </summary>
        /// <param name="n"><see cref="string"/> 的长度。</param>
        /// <param name="random">随机数生成器。</param>
        /// <returns>获取一个随机 <see cref="string"/>。</returns>
        static string RandomString(int n, Random random)
        {
            char[] value = new char[n];
            for (int i = 0; i < n; i++)
            {
                value[i] = (char)random.Next(char.MinValue + 10, char.MaxValue - 10);
            }
            return new string(value);
        }
    }
}
