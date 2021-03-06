﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sort;

namespace _2._1._32
{
    public partial class Form2 : Form
    {
        BaseSort sort;
        int n;
        double[] result;

        /// <summary>
        /// 构造一个绘图结果窗口。
        /// </summary>
        /// <param name="sort">用于做测试的排序算法。</param>
        /// <param name="n">用于测试的初始数据量。</param>
        public Form2(BaseSort sort, int n)
        {
            InitializeComponent();
            this.sort = sort;
            this.n = n;
            this.result = Test(n);
            this.timer1.Interval = 1000;
            this.timer1.Start();
        }

        /// <summary>
        /// 执行八次耗时测试，每次数据量翻倍。
        /// </summary>
        /// <param name="n">初始数据量。</param>
        /// <returns>测试结果数据。</returns>
        public double[] Test(int n)
        {
            double[] result = new double[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = SortCompare.TimeRandomInput(this.sort, n, 3);
                n *= 2;
            }
            return result;
        }

        /// <summary>
        /// 绘制曲线图。
        /// </summary>
        /// <param name="result">结果数组。</param>
        public void DrawPanel(double[] result)
        {
            Graphics graphics = this.CreateGraphics();
            graphics.TranslateTransform(0, this.ClientRectangle.Height);
            graphics.ScaleTransform(1, -1);
            Rectangle clientRect = this.ClientRectangle;
            Rectangle drawRect = new Rectangle(clientRect.X + 10, clientRect.Y + 10, clientRect.Width - 10, clientRect.Height - 10);

            PointF[] dataPoints = new PointF[result.Length];
            float unitX = (float)drawRect.Width / result.Length;
            float unitY = (float)(drawRect.Height / result.Max());
            SizeF pointSize = new SizeF(8, 8);
            for (int i = 0; i < result.Length; i++)
            {
                dataPoints[i] = new PointF(drawRect.Left + unitX * i, (float)(unitY * result[i]));
                graphics.FillEllipse(Brushes.Black, new RectangleF(dataPoints[i], pointSize));

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawPanel(this.result);
            this.timer1.Stop();
        }
    }
}
