﻿using System;
using System.IO;
using PriorityQueue;

namespace _2._4._42
{
    /*
     * 2.4.42
     * 
     * 堆的前序表示。
     * 用前序法而非级别表示一棵堆有序的树，
     * 并基于此实现堆排序。
     * 对于 N=10^3、10^6 和 10^9 大小的随机不重复数组，
     * 记录你的程序所使用的比较次数和标准实现所使用的比较次数。
     * 
     */
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("n\torigin\tpre-order\tRatio");

            int n = 1000;     // 当数据量到达 10^9 时会需要 2G 左右的内存
            int multiTen = 7;
            for (int i = 0; i < multiTen; i++)
            {
                Console.Write("10^" + (i + 3) + "\t");
                short[] data = GetRandomArray(n);
                BackupArray(data, i);       // 暂存数组
                long originCount = HeapAnalysis.Sort(data);
                Console.Write(originCount + "\t");

                RestoreArray(data, i);      // 恢复数组
                long preorderCount = HeapPreorderAnalysis.Sort(data);
                Console.WriteLine(preorderCount + "\t" + (float)preorderCount / originCount + "\t");
                n *= 10;
            }
        }

        static bool IsSorted(short[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                if (data[i] > data[i + 1])
                    return false;
            }
            return true;
        }

        static void BackupArray(short[] data, int index)
        {
            StreamWriter sw =
                File.CreateText
                (Environment.CurrentDirectory +
                Path.DirectorySeparatorChar +
                "data" + index + ".txt");
            for (int i = 0; i < data.Length; i++)
            {
                sw.WriteLine(data[i]);
            }
            sw.Flush();
            sw.Close();
        }

        static void RestoreArray(short[] data, int index)
        {
            StreamReader sr =
                File.OpenText
                (Environment.CurrentDirectory +
                Path.DirectorySeparatorChar +
                "data" + index + ".txt");
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = short.Parse(sr.ReadLine());
            }
            sr.Close();
        }

        static short[] GetRandomArray(int n)
        {
            short[] data = new short[n];
            for (int i = 0; i < n; i++)
            {
                data[i] = (short)random.Next();
            }
            return data;
        }

        static void SplitTree(int[] data, int p, int n)
        {
            int k = (int)(Math.Log10(n) / Math.Log10(2));   // 高度

            if (k == 0)
                return;
            Console.WriteLine("Spliting:");

            Console.Write(data[p] + " | ");
            int left = (int)Math.Pow(2, k - 1) - 1;
            int right = left;
            if (n - left < (int)Math.Pow(2, k))
            {
                // 叶子结点全在左侧
                left = n - right - 1;
            }
            else
            {
                left = (int)Math.Pow(2, k) - 1;
                right = n - left - 1;
            }

            // 输出
            int cursor = p + 1;
            for (int i = 0; i < left; i++)
            {
                Console.Write(data[cursor++] + " ");
            }
            Console.Write("| ");
            while (cursor < p + n)
                Console.Write(data[cursor++] + " ");
            Console.WriteLine();

            SplitTree(data, p + 1, left);
            SplitTree(data, p + left + 1, right);
        }
    }
}
