﻿namespace _1._4._27
{
    /// <summary>
    /// 用两个栈模拟的队列。
    /// </summary>
    /// <typeparam name="Item">队列中的元素。</typeparam>
    class StackQueue<Item>
    {
        Stack<Item> H;// 用于保存出队元素
        Stack<Item> T;// 用于保存入队元素

        /// <summary>
        /// 构造一个队列。
        /// </summary>
        public StackQueue()
        {
            this.H = new Stack<Item>();
            this.T = new Stack<Item>();
        }

        /// <summary>
        /// 将栈 T 中的元素依次弹出并压入栈 H 中。
        /// </summary>
        private void Reverse()
        {
            while (!this.T.IsEmpty())
            {
                this.H.Push(this.T.Pop());
            }
        }

        /// <summary>
        /// 将一个元素出队。
        /// </summary>
        /// <returns></returns>
        public Item Dequeue()
        {
            // 如果没有足够的出队元素，则将 T 中的元素移动过来
            if (this.H.IsEmpty())
            {
                Reverse();
            }

            return this.H.Pop();
        }

        /// <summary>
        /// 将一个元素入队。
        /// </summary>
        /// <param name="item">要入队的元素。</param>
        public void Enqueue(Item item)
        {
            this.T.Push(item);
        }
    }
}
