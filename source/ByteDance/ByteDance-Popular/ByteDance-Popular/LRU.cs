//设计和构建一个“最近最少使用”缓存，该缓存会删除最近最少使用的项目。缓存应该从键映射到值(允许你插入和检索特定键对应的值)，并在初始化时指定最大容量。当缓存被填满时，它应该删除最近最少使用的项目。

//它应该支持以下操作： 获取数据 get 和 写入数据 put 。

//获取数据 get(key) -如果密钥(key) 存在于缓存中，则获取密钥的值（总是正数），否则返回 - 1。
//写入数据 put(key, value) -如果密钥不存在，则写入其数据值。当缓存容量达到上限时，它应该在写入新数据之前删除最近最少使用的数据值，从而为新的数据值留出空间。

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/lru-cache-lcci
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
using System.Collections.Generic;

public class LRUCache
{
    public class DLinkedNode
    {
        public int key;
        public int val;
        public DLinkedNode next;
        public DLinkedNode prev;
        public DLinkedNode() { }
        public DLinkedNode (int _key, int _val)
        {
            key = _key;
            val = _val;
        }
    }

    Dictionary<int, DLinkedNode> pair;
    DLinkedNode Head = null;
    DLinkedNode Tail = null;
    public int Capacity;
     
    public LRUCache(int capacity)
    {
        pair = new Dictionary<int, DLinkedNode>(capacity);
        Capacity = capacity;

        // 使用伪头部和伪尾部节点
        Head = new DLinkedNode();
        Tail = new DLinkedNode();
        Head.next = Tail;
        Tail.prev = Head;

    }

    public int Get(int key)
    {
        if (pair.ContainsKey(key))
        {
            MoveToHead(pair[key]);
            return pair[key].val;
        }
        else
        {
            return -1;
        }
    }

    public void Put(int key, int value)
    {
        if (pair.ContainsKey(key))
        {
            pair[key].val = value;
            MoveToHead(pair[key]);
        }
        else
        {
            DLinkedNode entry = new DLinkedNode(key, value);
            AddToHead(entry);
            pair[key] = entry;
            if (Capacity < pair.Count)
            {
                DLinkedNode tail  = RemoveTail();
                pair.Remove(tail.key);
            }
        }

    }

    private void AddToHead(DLinkedNode node)
    {
        node.prev = Head;
        node.next = Head.next;
        Head.next.prev = node;
        Head.next = node;
    }

    private void RemoveNode(DLinkedNode node)
    {
        node.prev.next = node.next;
        node.next.prev = node.prev;
    }

    private void MoveToHead(DLinkedNode node)
    {
        RemoveNode(node);
        AddToHead(node);
    }

    private DLinkedNode RemoveTail()
    {
        DLinkedNode res = Tail.prev;
        RemoveNode(res);
        return res;
    }
}