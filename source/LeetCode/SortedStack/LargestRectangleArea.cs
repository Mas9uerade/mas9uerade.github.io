using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class CLargestRectangleArea
    {
        public int LargestRectangleArea(int[] heights)
        {
            int max_area = 0;

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < heights.Length; ++i)
            {
                while (stack.Count>0 && heights[stack.Peek()] >= heights[i])
                {
                    int index = stack.Peek();
                    stack.Pop();

                    int left_less_index = stack.Count==0 ? -1 : stack.Peek();
                    int area = heights[index] * (i - (left_less_index + 1));
                    if (area > max_area)
                    {
                        max_area = area;
                    }
                }
                stack.Push(i);
            }

            while (stack.Count> 0)
            {
                int index = stack.Peek();
                stack.Pop();

                int left_less_index = stack.Count==0 ? -1 : stack.Peek();

                int area = heights[index] * (heights.Length - (left_less_index + 1));
                if (area > max_area)
                {
                    max_area = area;
                }
            }

            return max_area;
        }
        
    }

    public class SortedStack<T> where T : IComparable<T>
    {
        public bool IsIncrement { get; private set; }

        private Stack<T> container;

        public SortedStack(bool _isIncrement)
        {
            IsIncrement = _isIncrement;
        }

        public void Push(T item)
        {
            while (container.Count > 0)
            {
                if ((container.Peek().CompareTo(item) >0) != IsIncrement)
                {
                    container.Push(item);
                    return;
                }
                else
                {
                    container.Pop();
                }
            }
        }

        public T Peek()
        {
            return container.Peek();
        }
    }
}
