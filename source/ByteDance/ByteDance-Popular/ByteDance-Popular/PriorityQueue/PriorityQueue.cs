using System;
using System.Collections.Generic;
//优先队列的实现方式是小根堆/大根堆
public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> data;

    public PriorityQueue()
    {
        this.data = new List<T>();
    }

    public PriorityQueue(int capacity)
    {
        this.data = new List<T>(capacity);
    }

    public int Count
    {
        get
        {
            return data.Count;
        }
    }

    public void Enqueue(T item)
    {
        data.Add(item);
        int childIndex = data.Count - 1;
        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;
            //此时已经完成了小节点的上浮
            if (data[childIndex].CompareTo(data[parentIndex]) >= 0)
            {
                break;
            }
            //否则交换节点
            T tmp = data[childIndex];
            data[childIndex] = data[parentIndex];
            data[parentIndex] = tmp;
            childIndex = parentIndex;
        }
    }

    public T Peek()
    {
        return data[0];
    }

    public T Dequue()
    {
        //交换节点数据去尾，减少时间复杂度
        int lastIndex = data.Count - 1;
        T frontItem = data[0];   // fetch the front
        data[0] = data[lastIndex];
        data.RemoveAt(lastIndex);
        --lastIndex;
        int parentIndex = 0;

        //小根堆维护
        while (true)
        {
            int childIndex = parentIndex * 2 + 1; // left child index of parent
            if (childIndex > lastIndex) break;  // no children so done
            int rightChild = childIndex + 1;     // right child
                                                 // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
            if (rightChild <= lastIndex && data[rightChild].CompareTo(data[childIndex]) < 0)
            {
                childIndex = rightChild;
            }
            // parent is smaller than (or equal to) smallest child so done
            if (data[parentIndex].CompareTo(data[childIndex]) <= 0)
            {
                break;
            }
            T tmp = data[parentIndex];
            data[parentIndex] = data[childIndex];
            data[childIndex] = tmp; // swap parent and child
            parentIndex = childIndex;
        }
        return frontItem;
    }
}
