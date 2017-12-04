﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._1._23
{
    /*
     * 2.1.23
     * 
     * 纸牌排序。
     * 请几位朋友分别将一副扑克牌排序（见练习2.1.13）。
     * 仔细观察并记录他们所使用的方法。
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            // 方法多种多样。
            // 首先是冒泡，见习题 2.1.13
            // 插入排序也可以，如下：
            //      从前往后不断翻牌，
            //      对于翻到的每张牌，一直和之前的牌交换，
            //      直至前面的牌比它小或者它已经是第一张了。
            // 也可以用基数排序
            //      从前向后依次翻开牌，
            //      按照花色分成四堆，
            //      然后按花色从大到小重新排列。
            // 比较符合直觉的是选择排序
            //      寻找最小的牌并放到第一位，
            //      寻找范围向右缩减一位，重复上一步，直到最后一张。
            // 还有其他方法，这里不再赘述。
        }
    }
}
