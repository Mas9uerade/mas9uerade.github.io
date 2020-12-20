// 42. 接雨水 问题 -  单调栈
using System;
using System.Collections.Generic;

namespace ByteDancePopular
{
    public partial class Solution
    {
        public int Trap(int[] height)
        {
            //单调递减的栈
            Stack<int> trapArea = new Stack<int>();
            int ret = 0, current = 0;
            Stack<int> stack = new Stack<int>();

            while (current < height.Length)
            {
                while (stack.Count>0 && height[current] > height[stack.Peek()])
                {
                    int top = stack.Pop();
                    if (stack.Count ==0)
                    {
                        break;
                    }
                        
                    int distance = current - stack.Peek() - 1;
                    int bounded_height = Math.Min(height[current], height[stack.Peek()]) - height[top];
                    ret += distance * bounded_height;
                }
                stack.Push(current++);
            }
            return ret;
        }
    }
}
