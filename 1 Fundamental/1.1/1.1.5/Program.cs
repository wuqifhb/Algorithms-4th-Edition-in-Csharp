﻿using System;

namespace _1._1._5
{
    /*
     * 1.1.5
     * 
     * 编写一段程序，
     * 如果 double 类型的变量 x 和 y 都严格位于 0 和 1 之间则打印 true
     * 否则打印 false。
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            // 修改这两个值进行测试
            double x = 0.05;
            double y = 0.01;

            if (x > 0 && x < 1 && y > 0 && y < 1)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }
        }
    }
}
