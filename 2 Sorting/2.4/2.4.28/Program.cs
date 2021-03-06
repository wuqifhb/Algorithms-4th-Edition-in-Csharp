﻿using System;
using System.Diagnostics;
using PriorityQueue;

namespace _2._4._28
{
    /*
     * 2.4.28
     * 
     * 选择过滤。
     * 编写一个 TopM 的用例，
     * 从标准输入读入坐标 (x, y, z)，从命令行得到值 M，
     * 然后打印出距离原点的欧几里得距离最小的 M 个点。
     * 在 N=10^8 且 M=10^4 时，预计程序的运行时间。
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            // m 不变的情况下算法是 O(n) 的
            // 因此预计时间是 n=10^5 的运行时间乘以 10^3 倍。
            int n = 100000, m = 10000;
            long prev = 0;
            for (int i = 0; i < 6; i++)
            {
                Console.Write("n= " + n + " m= " + m);
                long now = test(m, n);  // 获取当前 m,n 值的算法运行时间
                Console.Write("\t time=" + now);
                if (prev == 0)
                {
                    prev = now;
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("\tratio=" + (double)now / prev);
                    prev = now;
                }
                n *= 2;
            }
        }

        /// <summary>
        /// 进行一次测试。
        /// </summary>
        /// <param name="m">测试使用的 m 值。</param>
        /// <param name="n">测试使用的 n 值。</param>
        /// <returns></returns>
        static long test(int m, int n)
        {
            var pq = new MinPQ<EuclideanDistance3D>(m);
            int[] x = new int[n];
            int[] y = new int[n];
            int[] z = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                x[i] = random.Next();
                y[i] = random.Next();
                z[i] = random.Next();
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();// 开始计时
            for (int i = 0; i < m; i++)
            {
                // 先插入 m 个记录
                pq.Insert(new EuclideanDistance3D(x[i], y[i], z[i]));
            }
            for (int i = m; i < n; i++)
            {
                // 插入剩余 n-m 个记录
                pq.DelMin();
                pq.Insert(new EuclideanDistance3D(x[i], y[i], z[i]));
            }
            while (pq.IsEmpty())
                pq.DelMin();
            sw.Stop();// 停止计时
            return sw.ElapsedMilliseconds;
        }
    }
}
