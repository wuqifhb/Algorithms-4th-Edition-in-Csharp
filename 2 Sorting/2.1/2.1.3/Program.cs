﻿namespace _2._1._3
{
    /*
     * 2.1.3
     * 
     * 构造一个含有 N 个元素的数组，
     * 使选择排序（算法 2.1）运行过程中 
     * a[j] < a[min] （由此 min 会不断更新）成功的次数最大。
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            // 一个倒置的数列即可。
            // 9 8 7 6 5 4 3 2 1
            // 这样 a[j] < a[min] 条件满足的次数最多
            // 共 8 + 6 + 4 + 2 = 20 次
        }
    }
}
