﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace PriorityQueue
{
    /// <summary>
    /// 索引优先队列。
    /// </summary>
    /// <typeparam name="Key">优先队列中包含的元素。</typeparam>
    public class IndexMaxPQ<Key> : IEnumerable<int> where Key : IComparable<Key>
    {
        /// <summary>
        /// 优先队列中的元素。
        /// </summary>
        private int n;
        /// <summary>
        /// 索引最大堆。
        /// </summary>
        private int[] pq;
        /// <summary>
        /// pq 的逆索引，pq[qp[i]]=qp[pq[i]]=i
        /// </summary>
        private int[] qp;
        /// <summary>
        /// 实际元素。
        /// </summary>
        private Key[] keys;

        /// <summary>
        /// 建立指定大小的面向索引的最大堆。
        /// </summary>
        /// <param name="capacity"></param>
        public IndexMaxPQ(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            this.n = 0;
            this.keys = new Key[capacity + 1];
            this.pq = new int[capacity + 1];
            this.qp = new int[capacity + 1];
            for (int i = 0; i <= capacity; i++)
                this.qp[i] = -1;
        }

        /// <summary>
        /// 将与索引 <paramref name="i"/> 相关联的元素换成 <paramref name="k"/>。
        /// </summary>
        /// <param name="i">要修改关联元素的索引。</param>
        /// <param name="k">用于替换的新元素。</param>
        public void ChangeKey(int i, Key k)
        {
            if (!Contains(i))
                throw new ArgumentNullException("队列中没有该索引");
            this.keys[i] = k;
            Swim(this.qp[i]);
            Sink(this.qp[i]);
        }

        /// <summary>
        /// 确认堆包含某个索引 <paramref name="i"/>。
        /// </summary>
        /// <param name="i">要查询的索引。</param>
        /// <returns></returns>
        public bool Contains(int i) => this.qp[i] != -1;

        /// <summary>
        /// 删除索引 <paramref name="i"/> 对应的键值。
        /// </summary>
        /// <param name="i">要清空的索引。</param>
        public void Delete(int i)
        {
            if (!Contains(i))
                throw new ArgumentOutOfRangeException("index is not in the priority queue");
            int index = this.qp[i];
            Exch(index, this.n--);
            Swim(index);
            Sink(index);
            this.keys[i] = default(Key);
            this.qp[i] = -1;
        }

        /// <summary>
        /// 删除并获得最大元素所在的索引。
        /// </summary>
        /// <returns></returns>
        public int DelMax()
        {
            if (this.n == 0)
                throw new ArgumentOutOfRangeException("Priority Queue Underflow");
            int max = this.pq[1];
            Exch(1, this.n--);
            Sink(1);

            this.qp[max] = -1;
            this.keys[max] = default(Key);
            this.pq[this.n + 1] = -1;
            return max;
        }

        /// <summary>
        /// 将索引 <paramref name="i"/> 对应的键值减少为 <paramref name="key"/>。
        /// </summary>
        /// <param name="i">要修改的索引。</param>
        /// <param name="key">减少后的键值。</param>
        public void DecreaseKey(int i, Key key)
        {
            if (!Contains(i))
                throw new ArgumentOutOfRangeException("index is not in the priority queue");
            if (this.keys[i].CompareTo(key) <= 0)
                throw new ArgumentException("Calling IncreaseKey() with given argument would not strictly increase the Key");

            this.keys[i] = key;
            Sink(this.qp[i]);
        }

        /// <summary>
        /// 将索引 <paramref name="i"/> 对应的键值增加为 <paramref name="key"/>。
        /// </summary>
        /// <param name="i">要修改的索引。</param>
        /// <param name="key">增加后的键值。</param>
        public void IncreaseKey(int i, Key key)
        {
            if (!Contains(i))
                throw new ArgumentOutOfRangeException("index is not in the priority queue");
            if (this.keys[i].CompareTo(key) >= 0)
                throw new ArgumentException("Calling IncreaseKey() with given argument would not strictly increase the Key");

            this.keys[i] = key;
            Swim(this.qp[i]);
        }

        /// <summary>
        /// 将元素 <paramref name="v"/> 与索引 <paramref name="i"/> 关联。
        /// </summary>
        /// <param name="v">待插入元素。</param>
        /// <param name="i">需要关联的索引。</param>
        public void Insert(Key v, int i)
        {
            if (Contains(i))
                throw new ArgumentException("索引已存在");
            this.n++;
            this.qp[i] = this.n;
            this.pq[this.n] = i;
            this.keys[i] = v;
            Swim(this.n);
        }

        /// <summary>
        /// 堆是否为空。
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => this.n == 0;

        /// <summary>
        /// 获得与索引 <paramref name="i"/> 关联的元素。
        /// </summary>
        /// <param name="i">索引。</param>
        /// <returns></returns>
        public Key KeyOf(int i)
        {
            if (!Contains(i))
                throw new ArgumentNullException("队列中没有该索引");
            return this.keys[i];
        }

        /// <summary>
        /// 返回最大元素对应的索引。
        /// </summary>
        /// <returns></returns>
        public int MaxIndex()
        {
            if (this.n == 0)
                throw new ArgumentOutOfRangeException("Priority Queue Underflow");
            return this.pq[1];
        }

        /// <summary>
        /// 获得最大元素。
        /// </summary>
        /// <returns></returns>
        public Key MaxKey()
        {
            if (this.n == 0)
                throw new ArgumentOutOfRangeException("Priority Queue Underflow");
            return this.keys[this.pq[1]];
        }

        /// <summary>
        /// 返回堆的元素数量。
        /// </summary>
        /// <returns></returns>
        public int Size() => this.n;

        /// <summary>
        /// 比较第一个元素是否小于第二个元素。
        /// </summary>
        /// <param name="i">第一个元素。</param>
        /// <param name="j">第二个元素。</param>
        /// <returns></returns>
        private bool Less(int i, int j) 
            => this.keys[this.pq[i]].CompareTo(this.keys[this.pq[j]]) < 0;

        /// <summary>
        /// 交换两个元素。
        /// </summary>
        /// <param name="i">要交换的元素下标。</param>
        /// <param name="j">要交换的元素下标。</param>
        private void Exch(int i, int j)
        {
            int swap = this.pq[i];
            this.pq[i] = this.pq[j];
            this.pq[j] = swap;
            this.qp[this.pq[i]] = i;
            this.qp[this.pq[j]] = j;
        }

        /// <summary>
        /// 使下标为 <paramref name="k"/> 的元素上浮。
        /// </summary>
        /// <param name="k">上浮元素下标。</param>
        private void Swim(int k)
        {
            while (k > 1 && Less(k / 2, k))
            {
                Exch(k / 2, k);
                k /= 2;
            }
        }

        /// <summary>
        /// 使下标为 <paramref name="k"/> 元素下沉。
        /// </summary>
        /// <param name="k">需要下沉的元素。</param>
        private void Sink(int k)
        {
            while (k * 2 <= this.n)
            {
                int j = 2 * k;
                if (j < this.n && Less(j, j + 1))
                    j++;
                if (!Less(k, j))
                    break;
                Exch(k, j);
                k = j;
            }
        }

        /// <summary>
        /// 获取迭代器。
        /// </summary>
        /// <returns></returns>
        public IEnumerator<int> GetEnumerator()
        {
            IndexMaxPQ<Key> copy = new IndexMaxPQ<Key>(this.n);
            for (int i = 0; i < this.n; i++)
                copy.Insert(this.keys[this.pq[i]], this.pq[i]);

            while (!copy.IsEmpty())
                yield return copy.DelMax();
        }

        /// <summary>
        /// 获取迭代器。
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
