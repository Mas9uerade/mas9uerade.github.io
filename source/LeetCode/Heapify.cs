using System;
//二叉堆 用于最大堆和最小堆的生成
public class BinaryHeap
{
    public int[] tree;
    public BinaryHeapNode root;
    public int layer;
    public int nodeCount;

    public void Swap(int i, int j)
    {
        if (tree.Length <= i || tree.Length <=j)
        {
            return;
        }
        int tmp = tree[i];
        tree[i] = tree[j];
        tree[j] = tmp;
    }
    public void Heapify(int index, int count)
    {
        if (index > count)
        {
            return;
        }
        int c1 = index *2 +1;
        int c2 = index*2+2;
        int max = index;
        if (c1 < count && tree[c1] > tree[max])
        {
            max = c1;
        }
        if (c2 <count && tree[c2] > tree[max])
        {
            max  = c2;
        }
        if (max != index)
        {
            Swap(max, i);
            Heapify(count, max);
        }
    }

    //单个节点
    public class BinaryHeapNode
    {
        public int value;
        public BinaryHeapNode leftChild;
        public BinaryHeapNode rightChild;

        public void Swap(ref BinaryHeapNode node1, ref BinaryHeapNode node2)
        {
            int tmp = node2.value;
            node2.value = node1.value;
            node1.value = tmp;
        }

        //大根堆
        public void Heapify()
        {
            if (leftChild!=null && leftChild.value> value)
            {
                if (rightChild != null) 
                {
                    if (leftChild.value> rightChild.value)
                    {
                        Swap(ref this, ref this.leftChild);
                    }
                    else 
                    {
                        Swap(ref this, ref this.rightChild);
                    }
                }    
            }
            else if (rightChild != null && rightChild.value > value)
            {
                if (leftChild != null)
                {
                    Swap(ref this, ref this.rightChild);
                }
            }
        }
    }
}