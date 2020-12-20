using System;
using System.Collections.Generic;

public class SortedStack<T> where T : IComparable<T>
{

    public int Count { get { return container.Count; } }
    public bool IsIncrement { get; private set; }

    private Stack<T> container;

    public SortedStack(bool _isIncrement)
    {
        IsIncrement = _isIncrement;
        container = new Stack<T>();
    }

    public SortedStack(bool _isIncrement, int capacity)
    {
        IsIncrement = _isIncrement;
        container = new Stack<T>(capacity);
    }

    public void Push(T item)
    {
        if (container.Count > 0)
        {
            while (container.Count > 0)
            {
                if ((container.Peek().CompareTo(item) >= 0) != IsIncrement)
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
        else
        {
            container.Push(item);
        }
    }

    public T Pop()
    {
        return container.Pop();
    }

    public T Peek()
    {
        return container.Peek();
    }
}