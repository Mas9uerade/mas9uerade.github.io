
//407 接雨水 https://www.youtube.com/watch?v=cJayBq38VYw
// """
// 水从高出往低处流，某个位置储水量取决于四周最低高度，从最外层向里层包抄，用小顶堆动态找到未访问位置最小的高度
// """
using System;

namespace ByteDancePopular
{
    public partial class Solution
    {
        private class RainTrapBlock:IComparable<RainTrapBlock>
        {
            public int row;
            public int col;
            public int h;

            public RainTrapBlock(int _r, int _c, int _h)
            {
                row = _r;
                col = _c;
                h = _h;
            }

            public int CompareTo(RainTrapBlock other)
            {
                if (other.h > h)
                {
                    return -1;
                }
                else if (other.h < h)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

        }

        public int TrapRainWater(int[][] heightMap)
        {
            int m = heightMap.Length;
            int n = heightMap[0].Length;
            //防止扩容
            PriorityQueue<RainTrapBlock> border = new PriorityQueue<RainTrapBlock>(2*(m+n+20));
            bool[][] vistid = new bool[m][];
            //边界先入优先队列
            for (int i = 0; i < m; ++i)
            {
                vistid[i] = new bool[n];
                border.Enqueue(new RainTrapBlock(i, 0, heightMap[i][0]));
                vistid[i][0] = true;
                border.Enqueue(new RainTrapBlock(i, n - 1, heightMap[i][n - 1]));
                vistid[i][n - 1] = true;
            }

            for (int i = 1; i < n-1; ++i)
            {
                vistid[0][i] = true;
                vistid[m - 1][i] = true;
                border.Enqueue(new RainTrapBlock(0, i, heightMap[0][i]));
                border.Enqueue(new RainTrapBlock(m - 1, i, heightMap[m - 1][i]));
            }

            int currentMax = border.Peek().h;
            int trapSum = 0;
            while(border.Count >0)
            {
                RainTrapBlock block = border.Dequue();
                //边界无法储水
                if (block.row == 0 || block.row == m-1 || block.col == n-1 || block.col == 0)
                {
                    
                }
                else
                {
                    if (block.h < currentMax)
                    {
                        trapSum += currentMax - block.h;
                    }
                }
                currentMax = Math.Max(currentMax, block.h);
                //上
                if (block.row-1 >0 && block.row - 1< m-1 && !vistid[block.row - 1][block.col])
                {
                    border.Enqueue(new RainTrapBlock(block.row - 1, block.col, heightMap[block.row - 1][block.col]));
                    vistid[block.row - 1][block.col] = true;
                }
                //下
                if (block.row + 1 > 0 && block.row + 1 < m - 1 && !vistid[block.row + 1][block.col])
                {
                    border.Enqueue(new RainTrapBlock(block.row + 1, block.col, heightMap[block.row + 1][block.col]));
                    vistid[block.row + 1][block.col] = true;
                }
                //左
                if (block.col -1 > 0 && block.col - 1 < n - 1 && !vistid[block.row][block.col-1])
                {
                    border.Enqueue(new RainTrapBlock(block.row, block.col-1, heightMap[block.row][block.col-1]));
                    vistid[block.row][block.col-1] = true;
                }
                //右
                if (block.col + 1 > 0 && block.col + 1 < n - 1 && !vistid[block.row][block.col + 1])
                {
                    border.Enqueue(new RainTrapBlock(block.row, block.col +1, heightMap[block.row][block.col + 1]));
                    vistid[block.row][block.col+1] = true;
                }
            }
            return trapSum;
        }
    }
}